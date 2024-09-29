namespace EventGenerator.Services.Models
{
    /// <summary>
    /// Model for "New Day" message.
    /// </summary>
    public record NewDayMessageDto
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
