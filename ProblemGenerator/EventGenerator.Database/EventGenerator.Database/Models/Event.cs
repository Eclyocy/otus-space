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
        /// Generator ID.
        /// </summary>
        [Column("generator_id")]
        public Guid GeneratorId { get; set; }

        /// <summary>
        /// Event Level.
        /// </summary>
        [Column("event_level")]
        public int EventLevel { get; set; }
    }
}
