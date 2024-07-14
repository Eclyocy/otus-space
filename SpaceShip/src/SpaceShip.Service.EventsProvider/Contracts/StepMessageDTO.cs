namespace SpaceShip.Service.EventsConsumer.Contracts
{
    public class StepMessageDTO
    {
        public GeneratorDTO Generator { get; set; }

        public ShipDTO Ship { get; set; }
    }
}
