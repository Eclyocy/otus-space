using Shared.Enums;

namespace EventGenerator.Services.Models
{
    /// <summary>
    /// Model for trouble event message.
    /// </summary>
    public record EventMessageDto
    {
        /// <summary>
        /// Event ID.
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Ship ID.
        /// </summary>
        public Guid ShipId { get; set; }

        /// <summary>
        /// Event level.
        /// </summary>
        public EventLevel EventLevel { get; set; }
    }
}
