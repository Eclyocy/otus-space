using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Builder;
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
        public EventDto CreateEvent(CreateEventDto createEventRequest)
        {
            _logger.LogInformation("Create event by request {request}", createEventRequest);

            var eventBuilder = new EventBuilder(createEventRequest, _eventRepository, _logger);
            Event eventEntity = eventBuilder.Build();

            int spendTroublCoint = (int)eventEntity.EventLevel;

            GeneratorDto generatorEntity = _generatorService.SpendTroubleCoin(
                    eventEntity.GeneratorId,
                    spendTroublCoint);

            return _mapper.Map<EventDto>(eventEntity);
        }
    }
}
