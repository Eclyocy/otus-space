using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using Microsoft.Extensions.Logging;

namespace EventGenerator.Services.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        //private readonly IGeneratorService _generatorService;

        private readonly ILogger<EventService> _logger;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, ILogger<EventService> logger, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<EventDto> CreateEventAsync(Guid shipId)
        {
            _logger.LogInformation("Create event by Ship ID {shipId}", shipId);

            CreateEventDto createEventDto = new CreateEventDto();

            Random random = new Random();

            createEventDto.ShipId = shipId;
            createEventDto.troublecoint = random.Next(0, 10);

            //CreateSessionDto createSessionDto = await CreateSessionRequestAsync();
            Event eventRequest = _mapper.Map<Event>(createEventDto);
            Event event_ = _eventRepository.Create(eventRequest);

            return _mapper.Map<EventDto>(event_);
        }
    }
}