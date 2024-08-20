using System.ComponentModel.DataAnnotations.Schema;

namespace EventGenerator.Database.Models
{
    /// <summary>
    /// Generator model.
    /// </summary>
    [Table("generator")]
    public class Generator : BaseEntity
    {
        /// <summary>
        /// Generator ID.
        /// </summary>
        [Column("generator_id")]
        public Guid GeneratorId { get; set; }

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
