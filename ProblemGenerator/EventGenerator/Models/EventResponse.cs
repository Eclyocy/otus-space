﻿namespace EventGenerator.API.Models
{
    /// <summary>
    /// Event model.
    /// </summary>
    public class EventResponse
    {
        /// <summary>
        /// Generator ID.
        /// </summary>
        public Guid GeneratorId { get; set; }

        /// <summary>
        /// Event ID.
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Event Coint.
        /// </summary>
        public int EventCoint { get; set; }
    }
}
