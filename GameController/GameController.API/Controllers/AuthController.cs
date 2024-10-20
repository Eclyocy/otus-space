using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using GameController.API.Models.Auth;
using GameController.Services.Interfaces;
using GameController.Services.Models.Auth;
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
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;
        private readonly AuthService _authService;
        private readonly IMapper _mapper;

        // private readonly IConfiguration _configuration;
        public AuthController(JwtService jwtService, IUserService userService, AuthService authService, IMapper mapper)
        {
            _jwtService = jwtService;
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            TokenResponseDto result = _authService.ValidateUser(_mapper.Map<LoginDto>(login));
            return Ok(_mapper.Map<TokenResponse>(result));
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] TokenRequest tokenModel)
        {
            Console.WriteLine($"Jwt:Key: {_jwtService.GetConfigurationKey()}");  // Вызов метода для проверки конфигурации

            // Здесь можно добавить логику проверки рефреш токена, например, хранить их в базе данных
            var principal = GetPrincipalFromExpiredToken(tokenModel.Token);
            if (principal == null)
            {
                return BadRequest("Invalid token");
            }

            var newJwtToken = _jwtService.GenerateTokens(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return Ok(newJwtToken);
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
