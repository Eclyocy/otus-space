namespace EventGenerator.Services.Models.Generator
{
    /// <summary>
    /// Model for get generator.
    /// </summary>
    internal class GetGeneratorDto
    {
        /// <summary>
        /// Generator ID.
        /// </summary>
        public Guid GeneratorId { get; set; }

        /// <summary>
        /// Ship ID.
        /// </summary>
        public Guid ShipId { get; set; }
    }
}
