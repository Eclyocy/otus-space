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
        /// Ship ID.
        /// </summary>
        [Column("ship_id")]
        public Guid ShipId { get; set; }

        /// <summary>
        /// Trouble coins.
        /// </summary>
        [Column("trouble_coins")]
        public int TroubleCoins { get; set; }

        /// <summary>
        /// Generated events.
        /// </summary>
        public virtual ICollection<Event> Events { get; set; }
    }
}
