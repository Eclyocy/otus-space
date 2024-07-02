namespace GameController.Services.Models.User
{
    /// <summary>
    /// Model for user creation.
    /// </summary>
    public record CreateUserDto
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
