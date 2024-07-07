namespace GameController.API.Models.User
{
    /// <summary>
    /// Model for user update.
    /// </summary>
    public class UpdateUserModel
    {
        /// <summary>
        /// User name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        public string? Password { get; set; }
    }
}
