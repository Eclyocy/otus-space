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
        TokenResponseDto ValidateUser(LoginDto loginDto);

        /// <summary>
        /// Retrieving refresh token.
        /// </summary>
        TokenResponseDto RefreshToken(TokenDto tokenModel);
    }
}
