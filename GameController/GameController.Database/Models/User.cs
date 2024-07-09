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
        /// User NameUser.
        /// </summary>
        [Column("nameuser")]
        public required string NameUser { get; set; }

        /// <summary>
        /// User HashPass.
        /// </summary>
        [Column("hashpass")]
        public required string HashPass { get; set; }

        /// <summary>
        /// User Sessions.
        /// </summary>
        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
