namespace EventGenerator.Services.Models.Event
{
    public record EventDto
    {
        public Guid EventId { get; set; }
        public Guid IdShip { get; set; }
        public int Troublecoint { get; set; }
    }
}
