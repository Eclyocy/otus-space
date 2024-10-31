using System.Security.Claims;
using GameController.Services.Models.Auth;

namespace GameController.Services.Interfaces
{
    /// <summary>
    /// Interface supplying JWT logic.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generate JWT for the user.
        /// </summary>
        TokenDto GenerateTokens(string username);

        /// <summary>
        /// Get principal from the token.
        /// </summary>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
