namespace GameController.API.Models.Auth
{
    /// <summary>
    /// Model for token refresh request.
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Refresh token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
