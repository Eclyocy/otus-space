using EventGenerator.Database.Models;
using EventGenerator.Services.Models.Event;

namespace EventGenerator.Services.Interfaces
{
    /// <summary>
    /// Interface for event building.
    /// </summary>
    public interface IEventBuilder
    {
        /// <summary>
        /// Create a new event.
        /// </summary>
        Event? Build(CreateEventDto createEventDto);
    }
}
