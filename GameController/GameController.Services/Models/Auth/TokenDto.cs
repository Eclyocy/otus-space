namespace GameController.Services.Models.Auth
{
    /// <summary>
    /// Model for JWT.
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// Access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh token.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Token type.
        /// </summary>
        public string TokenType { get; set; } = "Bearer";

        /// <summary>
        /// Expiry time of the access token in seconds.
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}
