using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Exceptions;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using Microsoft.Extensions.Logging;

namespace EventGenerator.Services.Services
{
    /// <summary>
    /// Service for working with event generator.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IGeneratorRepository _generatorRepository;

        private readonly ILogger<EventService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventService(
            IEventRepository eventRepository,
            IGeneratorRepository generatorRepository,
            ILogger<EventService> logger,
            IMapper mapper)
        {
            _eventRepository = eventRepository;
            _generatorRepository = generatorRepository;

            _logger = logger;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public EventDto CreateEvent(Guid generatorId)
        {
            _logger.LogInformation("Create event by Generator ID {generatorId}", generatorId);

            if (_generatorRepository.Get(generatorId) == null)
            {
                _logger.LogError("Generator {generatorId} not found.", generatorId);

                throw new NotFoundException($"Generator {generatorId} not found.");
            }

            Generator generator = _generatorRepository.Get(generatorId);

            Random random = new Random();
            int newEventLevel = random.Next(1, 3);
            Event eventRequest = new ();
            eventRequest.GenertatorId = generatorId;
            eventRequest.EventLevel = (newEventLevel >= generator.TroubleCoins) ? newEventLevel : 0;
            Event event_ = _eventRepository.Create(eventRequest);
            return _mapper.Map<EventDto>(event_);
        }
    }
}
