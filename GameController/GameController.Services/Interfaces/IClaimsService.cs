using System.Security.Claims;
using GameController.Services.Models.User;

namespace GameController.Services.Interfaces
{
    /// <summary>
    /// Interface for working with claims.
    /// </summary>
    public interface IClaimsService
    {
        /// <summary>
        /// Get claims for the user.
        /// </summary>
        List<Claim> GetClaims(UserDto user);
    }
}
