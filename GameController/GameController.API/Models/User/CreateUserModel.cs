namespace GameController.API.Models.User
{
    /// <summary>
    /// Model for user creation.
    /// </summary>
    public record CreateUserModel
    {
        /// <summary>
        /// User name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        public required string Password { get; set; }
    }
}
