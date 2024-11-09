using AutoMapper;
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
        private readonly IEventBuilder _eventBuilder;

        private readonly ILogger<EventService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventService(
            IEventBuilder eventBuilder,
            ILogger<EventService> logger,
            IMapper mapper)
        {
            _eventBuilder = eventBuilder;

            _logger = logger;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public EventDto? CreateEvent(CreateEventDto createEventRequest)
        {
            _logger.LogInformation("Create event by request {request}", createEventRequest);

            Event? eventEntity = _eventBuilder.Build(createEventRequest);

            return eventEntity == null
                ? null
                : _mapper.Map<EventDto>(eventEntity);
        }
    }
}
