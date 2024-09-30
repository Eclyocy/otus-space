namespace EventGenerator.API.Models
{
    /// <summary>
    /// Model for generator creation.
    /// </summary>
    public class CreateGeneratorRequest
    {
        /// <summary>
        /// Ship ID.
        /// </summary>
        public Guid ShipId { get; set; }
    }
}
