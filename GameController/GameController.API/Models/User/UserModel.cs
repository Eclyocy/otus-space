namespace GameController.API.Models.User
{
    /// <summary>
    /// User model.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public required string Name { get; set; }
    }
}
