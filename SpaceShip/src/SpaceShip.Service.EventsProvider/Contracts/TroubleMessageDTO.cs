using Shared.Enums;

namespace SpaceShip.Service.EventsConsumer.Contracts
{
    /// <summary>
    /// Message DTO from EventGenerator.
    /// </summary>
    public class TroubleMessageDTO
    {
        /// <summary>
        /// Spaceship identifier.
        /// </summary>
        public Guid ShipId { get; set; }

        /// <summary>
        /// Event identifier.
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Event level.
        /// </summary>
        public EventLevel EventLevel { get; set; }
    }
}