namespace GameController.Services.Settings
{
    /// <summary>
    /// Settings for JWT authorization.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// JWT key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// JWT issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// JWT audience.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// JWT expiration time (in minutes).
        /// </summary>
        public int ExpirationMinutes { get; set; }
    }
}
