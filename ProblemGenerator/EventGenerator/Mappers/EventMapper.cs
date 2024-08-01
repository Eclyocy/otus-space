using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Models;
using EventGenerator.Services.Models.Event;

namespace EventGenerator.API.Mappers
{
    public class EventMapper : Profile
    {
        public EventMapper()
        {
            // Controller models -> Service models
            CreateMap<CreateEventRequest, CreateEventDto>();

            // Service models -> Controller models
            CreateMap<NewDayMessageDto, EventResponse>();
        }
    }
}
