using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Builder;
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

            var eventBuilder = new EventBuilder(createEventRequest, _eventRepository, _logger);
            Event eventEntity = eventBuilder.Build();

            return _mapper.Map<EventDto>(eventEntity);
        }
    }
}
