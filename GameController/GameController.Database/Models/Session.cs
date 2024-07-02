using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameController.Database.Models
{
    /// <summary>
    /// Session class.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// SessionId.
        /// </summary>
        [Key]
        [Column("sessionid")]
        public Guid SessionId { get; set; }

        /// <summary>
        /// UserId.
        /// </summary>
        [Column("userid")]
        public Guid UserId { get; set; }

        /// <summary>
        /// ShipId.
        /// </summary>
        [Column("shipid")]
        public Guid ShipId { get; set; }

        /// <summary>
        /// GeneratorId.
        /// </summary>
        [Column("generatorid")]
        public Guid GeneratorId { get; set; }

        /// <summary>
        /// User.
        /// </summary>
        public required User User { get; set; }
    }
}
