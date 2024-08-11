namespace EventGenerator.Services.Models
{
    public record NewDayMessageDto
    {
        public Guid EventId { get; set; }
        public Guid ShipId { get; set; }
    }
}
