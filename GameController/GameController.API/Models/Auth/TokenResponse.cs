using System.Text.Json.Serialization;

namespace GameController.API.Models.Auth
{
    /// <summary>
    /// Token response.
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Access token.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh token.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Token type.
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Identifies the token expiration time.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
