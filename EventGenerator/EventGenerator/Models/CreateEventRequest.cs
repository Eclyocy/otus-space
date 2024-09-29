namespace EventGenerator.API.Models
{
    /// <summary>
    /// Model for event creation.
    /// </summary>
    public class CreateEventRequest
    {
        /// <summary>
        /// Generator ID.
        /// </summary>
        public Guid GeneratorId { get; set; }
    }
}
