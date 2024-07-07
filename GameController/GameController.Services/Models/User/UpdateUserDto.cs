namespace GameController.Services.Models.User
{
    /// <summary>
    /// Model for user update.
    /// </summary>
    public record UpdateUserDto
    {
        /// <summary>
        /// User name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User password hash.
        /// </summary>
        public string PasswordHash { get; set; }
    }
}
