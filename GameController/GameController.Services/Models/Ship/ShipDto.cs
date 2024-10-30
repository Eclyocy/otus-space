using Newtonsoft.Json;
using Shared.Enums;

namespace GameController.Services.Models.Ship
{
    /// <summary>
    /// Model for space ship.
    /// </summary>
    public class ShipDto
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
        [JsonProperty("step")]
        public short Day { get; set; }

        /// <summary>
        /// Space ship state.
        /// </summary>
        public ShipState State { get; set; }

        /// <summary>
        /// Space ship resources.
        /// </summary>
        public List<ShipResourceDto> Resources { get; set; }
    }
}
