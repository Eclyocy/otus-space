using Newtonsoft.Json;

namespace GameController.Services.Models.Generator
{
    /// <summary>
    /// Model for generator.
    /// </summary>
    public record GeneratorDto
    {
        /// <summary>
        /// Generator ID.
        /// </summary>
        [JsonProperty("generatorId")]
        public Guid Id { get; set; }

        /// <summary>
        /// Space ship ID.
        /// </summary>
        [JsonProperty("shipId")]
        public Guid ShipId { get; set; }

        /// <summary>
        /// Generator trouble coins.
        /// </summary>
        public int TroubleCoins { get; set; }
    }
}
