namespace GameController.Services.Models.Message
{
    /// <summary>
    /// Model for messages for 'new day' events.
    /// </summary>
    public record NewDayMessage
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
