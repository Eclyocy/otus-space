using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Helpers;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using Microsoft.Extensions.Logging;
using Shared.Enums;

namespace EventGenerator.Services.Builders
{
    /// <summary>
    /// Event builder.
    /// </summary>
    public class EventBuilder : IEventBuilder
    {
        private readonly IEventRepository _eventRepository;

        private readonly ILogger<EventBuilder> _logger;

        private readonly Random _random;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventBuilder(
            IEventRepository eventRepository,
            ILogger<EventBuilder> logger,
            Random random)
        {
            _eventRepository = eventRepository;

            _logger = logger;
            _random = random;
        }

        /// <inheritdoc/>
        public Event? Build(CreateEventDto createEventDto)
        {
            _logger.LogInformation(
                "Creating an event for generator {generatorId} with {troubleCoins} trouble coins.",
                createEventDto.GeneratorId,
                createEventDto.TroubleCoins);

            EventLevel? maxEventLevel = TroubleCoinsConverter.ConvertTroubleCoins(createEventDto.TroubleCoins);

            if (maxEventLevel == null)
            {
                _logger.LogInformation("Cannot generate an event.");

                return null;
            }

            int generatedEventLevel = _random.Next(0, (int)maxEventLevel.Value + 1);

            if (generatedEventLevel == 0)
            {
                _logger.LogInformation("Decided to skip event generating.");

                return null;
            }

            return _eventRepository.Create(
                new()
                {
                    GeneratorId = createEventDto.GeneratorId,
                    EventLevel = (EventLevel)generatedEventLevel
                });
        }
    }
}
