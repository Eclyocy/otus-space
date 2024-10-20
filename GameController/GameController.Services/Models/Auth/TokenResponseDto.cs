namespace GameController.API.Models.Auth
{
    /// <summary>
    /// TokenResponseDto.
    /// </summary>
    public class TokenResponseDto
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
        /// Type of the token, usually "Bearer".
        /// </summary>
        public string TokenType { get; set; } = "Bearer";

        /// <summary>
        /// Expiry time of the access token in seconds.
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}
