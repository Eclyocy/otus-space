namespace GameController.API.Models.Session
{
    /// <summary>
    /// Model for session creation.
    /// </summary>
    public record CreateSessionRequest
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public Guid UserId { get; set; }

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
