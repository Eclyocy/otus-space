namespace GameController.API.Models.Auth
{
    public class TokenResponseOIDC
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; } = "Bearer";
        public int expires_in { get; set; }
    }
}
