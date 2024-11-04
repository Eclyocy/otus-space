namespace GameController.Services.Models.Auth
{
    /// <summary>
    /// Model for user login request.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        public string Password { get; set; }
    }
}
