namespace SpaceShip.Service.EventsConsumer.Contracts
{
    /// <summary>
    /// Message DTO from EventGenerator.
    /// </summary>
    public class TroubleMessageDTO
    {
        /// <summary>
        /// Event level.
        /// </summary>
        public int EventLevel { get; set; }
    }
}
