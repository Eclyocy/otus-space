using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Models;

namespace EventGenerator.API.Helpers
{
    public class EventMapper : Profile
    {
        public EventMapper()
        {
            // Controller models -> Service models
            CreateMap<CreateGeneratorRequest, CreateEventDto>();

            // Service models -> Controller models
            CreateMap<CreateGeneratorResponse, NewDayMessageDto>();
        }
    }
}
