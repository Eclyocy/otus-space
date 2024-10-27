using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameController.Services.Exceptions;
using GameController.Services.Helpers;
using GameController.Services.Interfaces;
using GameController.Services.Models.Auth;
using GameController.Services.Models.User;
using Microsoft.IdentityModel.Tokens;

namespace GameController.Services.Services
{
    /// <summary>
    /// AuthorizationService.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly JwtService _jwtService;
        private readonly IUserService _userService;

        /// <summary>
        /// AuthorizationService.
        /// </summary>
        public AuthService(JwtService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        /// <summary>
        /// AuthorizationService.
        /// </summary>
        public TokenResponseDto ValidateUser(LoginDto loginDto)
        {
            UserDto user = _userService.GetUserByName(loginDto.Username);

            if (HashHelper.HashPassword(loginDto.Password) != user.PasswordHash)
            {
                throw new UnauthorizedException("Wrong password!");
            }

            return _jwtService.GenerateTokens(user.Id.ToString());
        }

        /// <summary>
        /// Retrieving refresh token.
        /// </summary>
        public TokenResponseDto RefreshToken(TokenDto tokenModel)
        {
            var principal = GetPrincipalFromExpiredToken(tokenModel.Token);
            if (principal == null)
            {
               throw new UnauthorizedException("Invalid token");
            }

            if (principal.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                throw new UnauthorizedException("Invalid token");
            }

            var newJwtToken = _jwtService.GenerateTokens(principal.FindFirst(ClaimTypes.NameIdentifier)!.Value); // не понятно почему ругается, что может быть null

            return newJwtToken;
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
