using GameController.Services.Models.Auth;

namespace GameController.Services.Interfaces
{
    /// <summary>
    /// Interface for working with authentication.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticate the user.
        /// </summary>
        TokenDto Authenticate(LoginDto loginDto);

        /// <summary>
        /// Retrieve a refreshed token.
        /// </summary>
        TokenDto RefreshToken(RefreshTokenDto tokenModel);
    }
}
