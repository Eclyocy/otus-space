using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameController.Database.Models
{
    public class Session
    {
        [Key, Column("sessionid")]
        public Guid SessionId { get; set; }

        [Column("userid")]
        public Guid UserId { get; set; }

        [Column("shipid")]
        public Guid ShipId { get; set; }

        [Column("generatorid")]
        public Guid GeneratorId { get; set; }

        public required User User { get; set; }
    }
}
