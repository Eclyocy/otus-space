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
        /// Genertator ID.
        /// </summary>
        [Column("genertator_id")]
        public Guid GenertatorId { get; set; }

        /// <summary>
        /// Event ID.
        /// </summary>
        [Column("event_id")]
        public Guid EventId { get; set; }

        /// <summary>
        /// Event Coint.
        /// </summary>
        [Column("event_coint")]
        public int EventCoint { get; set; }
    }
}
