using System.ComponentModel.DataAnnotations.Schema;

namespace EventGenerator.Database.Models
{
    [Table("event")]
    public class Event
    {
        [Column("generatorid")]
        public Guid GeneratorId { get; set; }

        [Column("shipid")]
        public Guid ShipId { get; set; }

        [Column("troublecoint")]
        public int troublecoint { get; set; }
    }
}
