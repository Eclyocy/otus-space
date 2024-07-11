using System.ComponentModel.DataAnnotations.Schema;

namespace GameController.Database.Models
{
    /// <summary>
    /// User model.
    /// </summary>
    [Table("users")]
    public class User : BaseEntity
    {
        /// <summary>
        /// User name.
        /// </summary>
        [Column("name")]
        public required string Name { get; set; }

        /// <summary>
        /// User password hash.
        /// </summary>
        [Column("password_hash")]
        public required string PasswordHash { get; set; }

        /// <summary>
        /// User Sessions.
        /// </summary>
        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
