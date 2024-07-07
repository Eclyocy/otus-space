namespace GameController.Database.Models
{
    /// <summary>
    /// Session model.
    /// </summary>
    public class Session : BaseEntity
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
