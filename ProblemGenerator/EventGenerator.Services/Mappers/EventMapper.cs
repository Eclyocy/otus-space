using AutoMapper;
using EventGenerator.Services.Models;
using EventGenerator.Da.Models;

namespace EventGenerator.Services.Mappers
{
    public class EventMapper : Profile
    {
        public EventMapper()
        {
            // Service models -> Database models
            CreateMap<CreateEventDto, Event>();

            // Database models -> Service models
            CreateMap<Event, CreateEventDto>()
                .ForMember(x => x.ShipId, opt => opt.MapFrom(x => x.Id));
        }
    }
}
