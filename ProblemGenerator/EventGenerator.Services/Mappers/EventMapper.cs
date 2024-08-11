using AutoMapper;
using EventGenerator.Database.Models;
using EventGenerator.Services.Models.Event;

namespace EventGenerator.Services.Mappers
{
    public class EventMapper : Profile
    {
        public EventMapper()
        {
            // Service models -> Database models
            CreateMap<CreateEventDto, Event>();

            // Database models -> Service models
            CreateMap<Event, EventDto>()
                .ForMember(x => x.EventId, opt => opt.MapFrom(x => x.EventId));
        }
    }
}
