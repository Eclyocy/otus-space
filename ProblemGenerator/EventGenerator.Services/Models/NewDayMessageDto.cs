namespace EventGenerator.Services.Models
{
    public record NewDayMessageDto
    {
        public Guid GeneratorId { get; set; }
        public Guid ShipId { get; set; }
    }
}
