using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameController.Database.Models
{
    /// <summary>
    /// User class.
    /// </summary>
    [Table("users")]
    public class User
    {
        /// <summary>
        /// User UserId.
        /// </summary>
        [Key]
        [Column("userid")]
        public Guid UserId { get; set; }

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
