namespace GameController.Services.Models.Auth
{
    /// <summary>
    /// Model for token refresh request.
    /// </summary>
    public class RefreshTokenDto
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
