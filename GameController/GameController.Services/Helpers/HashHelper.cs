using System.Security.Cryptography;
using System.Text;

namespace GameController.Services.Helpers
{
    /// <summary>
    /// Helper for password hashing.
    /// </summary>
    public static class HashHelper
    {
        #region public methods

        /// <summary>
        /// Generates a hash of the password using the SHA256 algorithm.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password in Base64 format.</returns>
        public static string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = SHA256.HashData(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        #endregion
    }
}
