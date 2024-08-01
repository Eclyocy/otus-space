namespace EventGenerator.Services.Models.Event
{
    public record EventDto
    {
        public Guid GeneratorId { get; set; }
        public Guid IdShip { get; set; }
        public int troublecoint { get; set; }
    }
}
