namespace GameController.API.Models.Auth
{
    /// <summary>
    /// Model for login request.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// User name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; set; }
    }
}
