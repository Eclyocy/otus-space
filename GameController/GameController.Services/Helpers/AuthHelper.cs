using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GameController.Services.Helpers
{
    /// <summary>
    /// Authorization helper.
    /// </summary>
    public static class AuthHelper
    {
        #region public methods

        /// <summary>
        /// Generate a <see cref="SymmetricSecurityKey"/> from the specified <paramref name="key"/>.
        /// </summary>
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new(Encoding.UTF8.GetBytes(key));
        }

        #endregion
    }
}
