using System.Security.Claims;
using GameController.Services.Interfaces;
using GameController.Services.Models.User;

namespace GameController.Services.Services
{
    /// <summary>
    /// Service for working with claims.
    /// </summary>
    public class ClaimsService : IClaimsService
    {
        /// <inheritdoc/>
        public List<Claim> GetClaims(UserDto user)
        {
            return [
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
            ];
        }
    }
}
