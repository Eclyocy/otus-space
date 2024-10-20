using System.Security.Cryptography;
using System.Text;

namespace GameController.Services.Helpers
{
    /// <summary>
    /// Class containing helper methods for password hashing.
    /// </summary>
    public class HashHelper
    {
        #region public methods

        /// <summary>
        /// Generates a hash of the password using the SHA256 algorithm.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password in Base64 format.</returns>
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        #endregion
    }
}
