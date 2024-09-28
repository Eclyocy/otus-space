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
        /// Event Level.
        /// </summary>
        public int EventLevel { get; set; }
    }
}
