namespace GameController.Services.Models.Session
{
    /// <summary>
    /// Model for session creation.
    /// </summary>
    public record CreateSessionDto
    {
        /// <summary>
        /// Ship ID.
        /// </summary>
        public Guid ShipId { get; set; }

        /// <summary>
        /// Generator ID.
        /// </summary>
        public Guid GeneratorId { get; set; }
    }
}
