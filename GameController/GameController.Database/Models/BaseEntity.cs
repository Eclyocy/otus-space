namespace GameController.Database.Models
{
    /// <summary>
    /// Base entity.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Entity ID.
        /// </summary>
        public Guid Id { get; set; }
    }
}
