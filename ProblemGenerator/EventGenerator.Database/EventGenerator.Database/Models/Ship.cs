using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGenerator.Database.Models
{
    [Table("ship")]
    public class Ship : BaseEntity
    {
        [Column("shipid")]
        public Guid ShipId { get; set; }
    }
}
