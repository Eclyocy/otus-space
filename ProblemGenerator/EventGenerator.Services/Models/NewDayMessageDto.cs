namespace EventGenerator.Services.Models
{
    /// <summary>
    /// Model for New Day Message
    /// </summary>
    public record NewDayMessageDto
    {
        /// <summary>
        /// Generator Id.
        /// </summary>
        public Guid GeneratorId { get; set; }

        /// <summary>
        /// Ship Id.
        /// </summary>
        public Guid ShipId { get; set; }
    }
}
