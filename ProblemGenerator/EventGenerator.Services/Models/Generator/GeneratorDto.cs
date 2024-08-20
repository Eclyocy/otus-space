namespace EventGenerator.Services.Models.Generator
{
    /// <summary>
    /// Model for generator.
    /// </summary>
    public record GeneratorDto
    {
        /// <summary>
        /// Ship ID.
        /// </summary>
        public Guid ShipId { get; set; }

        /// <summary>
        /// Generator ID.
        /// </summary>
        public Guid GeneratorId { get; set; }

        /// <summary>
        /// Troublecoint.
        /// </summary>
        public int Troublecoint { get; set; }
    }
}
