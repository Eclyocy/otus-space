using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Helpers;
using EventGenerator.Services.Models.Event;
using EventGenerator.Services.Services;
using Microsoft.Extensions.Logging;
using Shared.Enums;

namespace EventGenerator.Services.Builder
{
    public class EventBuilder
    {
        private readonly CreateEventDto _createEventDto;
        private readonly IEventRepository _eventRepository;
        private readonly ILogger<EventService> _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventBuilder(
            CreateEventDto createEventDto,
            IEventRepository eventRepository,
            ILogger<EventService> logger)
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
        public Event? Build()
        {
            _logger.LogInformation(
                "Trying to create an event for {generatorId} generator an event with {troubleCoins} trouble coins.",
                _createEventDto.GeneratorId,
                _createEventDto.TroubleCoins);

            EventLevel? maxEventLevel = TroubleCoinsConverter.ConvertTroubleCoins(_createEventDto.TroubleCoins);

            if (maxEventLevel == null)
            {
                _logger.LogInformation("Cannot generate an event.");

                return null;
            }

            Random random = new();
            int generatedEventLevel = random.Next(0, (int)maxEventLevel.Value + 1);

            if (generatedEventLevel == 0)
            {
                _logger.LogInformation("Decided to skip event generating.");

                return null;
            }

            return _eventRepository.Create(
                new()
                {
                    GeneratorId = _createEventDto.GeneratorId,
                    EventLevel = (EventLevel)generatedEventLevel
                });
        }
    }
}
