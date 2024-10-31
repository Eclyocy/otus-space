using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Models.Event;
using Microsoft.Extensions.Logging;

namespace EventGenerator.Services.Builder
{
    public class EventBuilder
    {
        private readonly CreateEventDto _createEventDto;
        private readonly IEventRepository _eventRepository;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventBuilder(
            CreateEventDto createEventDto,
            IEventRepository eventRepository,
            ILogger logger)
        {
            _createEventDto = createEventDto;
            _eventRepository = eventRepository;
            _logger = logger;
        }

        /// <summary>
        /// Event level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Create new event.
        /// </summary>
        public Event Build()
        {
            EventLevelDto? maxEventLevel = GetMaxEventLevel(_createEventDto.FreeTroubleCoins);

            Random random = new();
            EventLevel generatedEventLevel = (EventLevel)random.Next(0, (int)maxEventLevel.Value);

            var newevent = _eventRepository.Create(new Event()
            {
                EventLevel = generatedEventLevel,
            });

            return newevent;
        }

        /// <summary>
        /// Get maximum level of event the generator can produce.
        /// </summary>
        private static EventLevelDto? GetMaxEventLevel(int troubleCoins)
        {
            return troubleCoins switch
            {
                0 => null,
                1 => EventLevelDto.Low,
                2 => EventLevelDto.Medium,
                _ => EventLevelDto.High
            };
        }
    }
}
