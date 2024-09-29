namespace GameController.Services.Models.Generator
{
    /// <summary>
    /// Model for generator creation.
    /// </summary>
    public record CreateGeneratorDto
    {
        /// <summary>
        /// Ship ID.
        /// </summary>
        public Guid ShipId { get; set; }
    }
}
