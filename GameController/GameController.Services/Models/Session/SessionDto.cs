namespace GameController.Services.Models.Session
{
    /// <summary>
    /// Session model.
    /// </summary>
    public record SessionDto
    {
        /// <summary>
        /// Session ID.
        /// </summary>
        public Guid SessionId { get; set; }

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
