using System.ComponentModel.DataAnnotations.Schema;

namespace GameController.Database.Models
{
    /// <summary>
    /// Session model.
    /// </summary>
    [Table("sessions")]
    public class Session : BaseEntity
    {
        /// <summary>
        /// User ID.
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// ShipId.
        /// </summary>
        [Column("ship_id")]
        public Guid ShipId { get; set; }

        /// <summary>
        /// GeneratorId.
        /// </summary>
        [Column("generator_id")]
        public Guid GeneratorId { get; set; }

        /// <summary>
        /// User.
        /// </summary>
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
