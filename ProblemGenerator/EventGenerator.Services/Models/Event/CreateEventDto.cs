using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGenerator.Services.Models.Event
{
    public class CreateEventDto
    {
        /// <summary>
        /// Ship ID.
        /// </summary>
        public Guid ShipId { get; set; }
        /// <summary>
        /// Event ID.
        /// </summary>
        public Guid EventId { get; set; }
        /// <summary>
        /// troublecoint.
        /// </summary>
        public int troublecoint { get; set; }
    }
}
