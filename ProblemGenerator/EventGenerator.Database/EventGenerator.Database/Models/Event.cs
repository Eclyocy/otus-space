using System.ComponentModel.DataAnnotations.Schema;

namespace EventGenerator.Database.Models
{
    /// <summary>
    /// Event model.
    /// </summary>
    [Table("event")]
    public class Event : BaseEntity
    {
        /// <summary>
        /// Event ID.
        /// </summary>
        [Column("event_id")]
        public Guid EventId { get; set; }

        /// <summary>
        /// Ship ID.
        /// </summary>
        [Column("ship_id")]
        public Guid ShipId { get; set; }

        /// <summary>
        /// Troublecoint.
        /// </summary>
        [Column("troublecoint")]
        public int Troublecoint { get; set; }
    }
}
