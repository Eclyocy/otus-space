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
        /// Trouble Coins.
        /// </summary>
        [Column("trouble_coins")]
        public int TroubleCoins { get; set; }
    }
}
