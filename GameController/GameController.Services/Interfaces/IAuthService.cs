using GameController.Services.Models.Auth;

namespace GameController.Services.Interfaces
{
    /// <summary>
    /// Interface for working with authentication.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Validates the user credentials.
        /// </summary>
        TokenDto ValidateUser(LoginDto loginDto);

        /// <summary>
        /// Retrieving refresh token.
        /// </summary>
        TokenDto RefreshToken(RefreshTokenDto tokenModel);
    }
}
