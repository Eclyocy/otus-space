using EventGenerator.Services.Models.Event;

namespace EventGenerator.Services.Interfaces
{
    /// <summary>
    /// Interface for working with events.
    /// </summary>
    public interface IEventService
     {
        /// <summary>
        /// Create an event.
        /// </summary>
        EventDto CreateEvent(Guid generatorId);
    }
}
