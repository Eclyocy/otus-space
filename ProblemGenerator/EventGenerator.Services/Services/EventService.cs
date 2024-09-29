using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Exceptions;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using EventGenerator.Services.Models.Generator;
using Microsoft.Extensions.Logging;

namespace EventGenerator.Services.Services
{
    /// <summary>
    /// Service for working with events.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        private readonly IGeneratorService _generatorService;

        private readonly ILogger<EventService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventService(
            IEventRepository eventRepository,
            IGeneratorService generatorService,
            ILogger<EventService> logger,
            IMapper mapper)
        {
            _eventRepository = eventRepository;

            _generatorService = generatorService;

            _logger = logger;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public EventDto CreateEvent(Guid generatorId)
        {
            _logger.LogInformation("Create event by Generator ID {generatorId}", generatorId);

            GeneratorDto generator = _generatorService.GetGenerator(generatorId);

            if (generator.TroubleCoins == 0)
            {
                _logger.LogWarning(
                    "Unable to create a new event for generator {generatorId}: no trouble coins.",
                    generatorId);

                throw new PreconditionFailedException($"Generator {generatorId} has insufficient funds.");
            }

            Random random = new();
            int eventLevel = random.Next(1, generator.TroubleCoins);

            Event eventRequest = new()
            {
                GeneratorId = generatorId,
                EventLevel = eventLevel
            };

            Event eventEntity = _eventRepository.Create(eventRequest);
            return _mapper.Map<EventDto>(eventEntity);
        }
    }
}
