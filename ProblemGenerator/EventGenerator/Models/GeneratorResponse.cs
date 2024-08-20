namespace EventGenerator.API.Models
{
    /// <summary>
    /// Generator model.
    /// </summary>
    public class GeneratorResponse
    {
        /// <summary>
        /// Generator ID.
        /// </summary>
        public Guid GeneratorId { get; set; }

        /// <summary>
        /// Ship ID.
        /// </summary>
        public Guid ShipId { get; set; }

        /// <summary>
        /// Troublecoint.
        /// </summary>
        public int Troublecoint { get; set; }
    }
}
