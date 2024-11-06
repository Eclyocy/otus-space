using Shared.Enums;

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
        /// Event level.
        /// </summary>
        public EventLevel EventLevel { get; set; }

        /// <summary>
        /// Event cost based on its <see cref="EventLevel"/>.
        /// </summary>
        public int EventCost { get; set; }
    }
}
