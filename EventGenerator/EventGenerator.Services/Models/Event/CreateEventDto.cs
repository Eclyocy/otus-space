namespace EventGenerator.Services.Models.Event
{
    /// <summary>
    /// Model for event creation.
    /// </summary>
    public class CreateEventDto
    {
        /// <summary>
        /// Generator ID.
        /// </summary>
        public Guid GeneratorId { get; set; }

        /// <summary>
        /// Event level.
        /// </summary>
        public int TroubleCoins { get; set; }
    }
}
