using System.ComponentModel.DataAnnotations.Schema;

namespace EventGenerator.Database.Models
{
    /// <summary>
    /// Ship model.
    /// </summary>
    [Table("ship")]
    public class Ship : BaseEntity
    {
        /// <summary>
        /// Ship ID.
        /// </summary>
        [Column("ship_id")]
        public Guid ShipId { get; set; }
    }
}