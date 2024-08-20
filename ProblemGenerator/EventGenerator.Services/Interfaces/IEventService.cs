using EventGenerator.Services.Models.Event;

namespace EventGenerator.Services.Interfaces
{
    /// <summary>
    /// Interface for working with event generator.
    /// </summary>
    public interface IEventService
     {
        /// <summary>
        /// Create a event.
        /// </summary>
        EventDto CreateEvent(Guid generatorpId);
    }
}
