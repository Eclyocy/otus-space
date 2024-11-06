using Shared.Enums;

namespace GameController.API.Models.Ship
{
    /// <summary>
    /// Model for space ship response.
    /// </summary>
    public class ShipResponse
    {
        /// <summary>
        /// Space ship ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Space ship name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Space ship current day.
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Space ship state.
        /// </summary>
        public ShipState State { get; set; }

        /// <summary>
        /// Value indicating the distance the space ship has traveled.
        /// </summary>
        public byte DistanceTraveled { get; set; }

        /// <summary>
        /// Value indicating the target distance the space ship has to cover.
        /// </summary>
        public byte DistanceTarget { get; set; }

        /// <summary>
        /// Space ship resources.
        /// </summary>
        public List<ShipResourceResponse> Resources { get; set; }
    }
}
