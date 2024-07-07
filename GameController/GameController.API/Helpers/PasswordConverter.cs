namespace GameController.API.Helpers
{
    /// <summary>
    /// Class for handling the password to password hash conversion.
    /// </summary>
    public static class PasswordConverter
    {
        #region public methods

        /// <summary>
        /// Convert password to password hash.
        /// </summary>
        /// <remarks>
        /// TODO: migrate to PasswordHasher from Microsoft.AspNetCore.Identity.
        /// </remarks>
        public static string ConvertToHash(string password)
        {
            return password;
        }

        #endregion
    }
}
