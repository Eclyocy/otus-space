namespace GameController.Services.Models.User
{
    /// <summary>
    /// User model.
    /// </summary>
    public record UserDto
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string Name { get; set; }
    }
}
