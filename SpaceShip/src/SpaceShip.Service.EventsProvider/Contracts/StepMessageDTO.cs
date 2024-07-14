namespace SpaceShip.Service.EventsConsumer.Contracts
{
    public class StepMessageDTO
    {
        public Guid GeneratorId { get; set; }

        public Guid ShipId { get; set; }
    }
}
