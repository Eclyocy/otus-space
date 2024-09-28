using AutoMapper;
using EventGenerator.Database.Models;
using EventGenerator.Services.Models.Event;

namespace EventGenerator.Services.Mappers
{
    /// <summary>
    /// Mappers for generated events models.
    /// </summary>
    public class EventMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EventMapper()
        {
            // Service models -> Database models
            CreateMap<CreateEventDto, Event>();

            // Database models -> Service models
            CreateMap<Event, EventDto>();
        }
    }
}
