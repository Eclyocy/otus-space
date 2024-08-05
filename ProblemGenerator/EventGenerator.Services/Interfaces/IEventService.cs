using EventGenerator.Services.Models.Event;

namespace EventGenerator.Services.Interfaces
{
    public interface IEventService
     {
        Task<EventDto> CreateEventAsync(Guid shipId);
    }
}
