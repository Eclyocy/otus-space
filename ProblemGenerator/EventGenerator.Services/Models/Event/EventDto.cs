namespace EventGenerator.Services.Models.Event
{
    /// <summary>
    /// Model for event.
    /// </summary>
    public record EventDto
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
