using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGenerator.Services.Models
{
    public record NewDayMessageDto
    {
        public Guid GeneratorId { get; set; }
        public Guid ShipId { get; set; }
    }
}
