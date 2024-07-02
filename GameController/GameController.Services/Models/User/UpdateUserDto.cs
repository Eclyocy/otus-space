namespace GameController.Services.Models.User
{
    /// <summary>
    /// Model for user update.
    /// </summary>
    public record UpdateUserDto
    {
        /// <summary>
        /// User id.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// User name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// User password hash.
        /// </summary>
        public required string PasswordHash { get; set; }
    }
}
