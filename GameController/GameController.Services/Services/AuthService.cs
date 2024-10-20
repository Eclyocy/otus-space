using GameController.API.Models.Auth;
using GameController.Services.Exceptions;
using GameController.Services.Helpers;
using GameController.Services.Interfaces;
using GameController.Services.Models.Auth;

namespace GameController.Services.Services
{
    /// <summary>
    /// AuthorizationService.
    /// </summary>
    public class AuthService
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
        public TokenResponseDto? ValidateUser(LoginDto loginDto)
        {
            var user = _userService.GetUserByName(loginDto.Username);
            if (HashHelper.HashPassword(loginDto.Password) != user!.PasswordHash)
            {
                throw new NotFoundException("Wrong password!");
            }

            var (token, refreshToken, expiresIn) = _jwtService.GenerateTokens(user!.Id.ToString());
            var tokenResponse = new TokenResponseDto
            {
                 AccessToken = token,
                 RefreshToken = refreshToken,
                 ExpiresIn = expiresIn
            };

            return tokenResponse;
        }
    }
}
