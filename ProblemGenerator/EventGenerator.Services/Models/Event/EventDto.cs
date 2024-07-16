using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGenerator.Services.Models.Event
{
    public record EventDto
    {
        public Guid EventId { get; set; }
    }
}
