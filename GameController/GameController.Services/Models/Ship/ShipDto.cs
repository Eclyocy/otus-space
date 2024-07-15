using Newtonsoft.Json;

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
        /// Space ship current day.
        /// </summary>
        [JsonProperty("step")]
        public int Day { get; set; }

        /// <summary>
        /// Space ship resources.
        /// </summary>
        public List<ShipResourceDto> Resources { get; set; }
    }
}
