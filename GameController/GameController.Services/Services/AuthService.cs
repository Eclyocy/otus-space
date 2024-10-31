using System.Security.Claims;
using GameController.Services.Exceptions;
using GameController.Services.Helpers;
using GameController.Services.Interfaces;
using GameController.Services.Models.Auth;
using GameController.Services.Models.User;

namespace GameController.Services.Services
{
    /// <summary>
    /// Service supplying authorization logic.
    /// </summary>
    public class AuthService : IAuthService
    {
        #region private fields

        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AuthService(
            IJwtService jwtService,
            IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public TokenDto Authenticate(LoginDto loginDto)
        {
            UserDto user = _userService.GetUserByName(loginDto.Username);

            if (HashHelper.HashPassword(loginDto.Password) != user.PasswordHash)
            {
                throw new UnauthorizedException($"Incorrect password supplied for user \"{loginDto.Username}\".");
            }

            return _jwtService.GenerateTokens(user.Id.ToString());
        }

        /// <inheritdoc/>
        public TokenDto RefreshToken(RefreshTokenDto tokenModel)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(tokenModel.Token);
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

        #endregion
    }
}
