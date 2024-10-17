using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using Microsoft.Extensions.Logging;

namespace EventGenerator.Services.Services
{
    /// <summary>
    /// Service for working with events.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        private readonly ILogger<EventService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventService(
            IEventRepository eventRepository,
            ILogger<EventService> logger,
            IMapper mapper)
        {
            _eventRepository = eventRepository;

            _logger = logger;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public EventDto CreateEvent(CreateEventDto createEventRequest)
        {
            _logger.LogInformation("Create event by request {request}", createEventRequest);

            Event eventEntity = _eventRepository.Create(_mapper.Map<Event>(createEventRequest));

            return _mapper.Map<EventDto>(eventEntity);
        }

        public List<EventDto> GetEvents(Guid generatorId)
        {
            _logger.LogInformation("Get event of user {userId}", generatorId);

            List<Event> events = _eventRepository.GetAllByGeneratorId(generatorId);

            return _mapper.Map<List<EventDto>>(events);
        }
    }
}
