using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using Microsoft.Extensions.Logging;

namespace EventGenerator.Services.Builder
{
    public class EventBuilder
    {
        private readonly IEventRepository _eventRepository;
        private readonly ILogger _logger;

        private EventLevel _level;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventBuilder(
            IEventRepository eventRepository,
            ILogger logger)
        {
            _eventRepository = eventRepository;
            _logger = logger;
        }

        /// <summary>
        /// Event level.
        /// </summary>
        public EventLevel Level { get; set; }

        /// <summary>
        /// Create level.
        /// </summary>
        public EventBuilder CreateLevel(EventLevel level)
        {
            _level = level;
            return this;
        }

        /// <summary>
        /// Create new event.
        /// </summary>
        public Event Build()
        {
            var newevent = _eventRepository.Create(new Event()
            {
                EventLevel = _level,
            });

            return newevent;
        }
    }
}
