using Shared.Enums;

namespace SpaceShip.Service.Models
{
    /// <summary>
    /// Trouble from EventGenerator.
    /// </summary>
    public class Trouble
    {
        /// <summary>
        /// Spaceship identifier.
        /// </summary>
        public Guid ShipId { get; set; }

        /// <summary>
        /// Failed resource.
        /// </summary>
        public ResourceType Resource { get; set; }

        /// <summary>
        /// Failure level.
        /// </summary>
        public EventLevel Level { get; set; }
    }
}
