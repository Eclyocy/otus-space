using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameController.API.Models.Auth;
using GameController.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GameController.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        // private readonly IConfiguration _configuration;
        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Username == "string" && login.Password == "string")
            {
                var (token, refreshToken, expiresIn) = _jwtService.GenerateTokens(login.Username);
                var tokenResponse = new TokenResponse
                {
                    AccessToken = token,
                    RefreshToken = refreshToken,
                    ExpiresIn = expiresIn
                };
                return Ok(tokenResponse);
            }

            return Unauthorized();
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] TokenModel tokenModel)
        {
            Console.WriteLine($"Jwt:Key: {_jwtService.GetConfigurationKey()}");  // Вызов метода для проверки конфигурации

            // Здесь можно добавить логику проверки рефреш токена, например, хранить их в базе данных
            var principal = GetPrincipalFromExpiredToken(tokenModel.Token);
            if (principal == null)
            {
                return BadRequest("Invalid token");
            }

            var newJwtToken = _jwtService.GenerateTokens(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // return Ok(new { token = newJwtToken });
            return Ok(new { token = newJwtToken.Token, refreshToken = newJwtToken.RefreshToken });
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult TestAuth()
        {
            var username = User.Identity.Name; // Извлечение информации о пользователе из токена
            return Ok($"Welcome {username}, you are authorized to play!");
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = _jwtService.GetKey();
            var issuer = _jwtService.GetIssuer();
            var audience = _jwtService.GetAudience();

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new ArgumentException("JWT configuration is missing one or more required values.");
            }

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
