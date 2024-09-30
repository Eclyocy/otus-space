using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Models.Event;

namespace EventGenerator.API.Mappers
{
    /// <summary>
    /// Mappings for event models.
    /// </summary>
    public class EventMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EventMapper()
        {
            // Controller models -> Service models
            CreateMap<CreateEventRequest, CreateEventDto>();

            // Service models -> Controller models
            CreateMap<EventDto, EventResponse>();
        }
    }
}
