using AutoMapper;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Helpers;
using EventGenerator.Services.Models.Event;
using EventGenerator.Database.Interfaces;
using Microsoft.Extensions.Logging;
using EventGenerator.Database.Models;

namespace EventGenerator.Services.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly IEventRepository _eventRepository;
        //private readonly IGeneratorService _generatorService;
        private readonly ILogger<GeneratorService> _logger;

        private readonly IMapper _mapper;

        public GeneratorService(IEventRepository eventRepository, ILogger<GeneratorService> logger, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public CreateEventDto CreateEvent(Guid shipId)
        {
            _logger.LogInformation("Create generator by Ship ID {shipId}", shipId);

            CreateEventDto createEventDto = new CreateEventDto();

            Random random = new Random();

            createEventDto.ShipId = shipId;
            createEventDto.troublecoint = random.Next(0, 10);

            return _mapper.Map<CreateEventDto>(createEventDto);
        }
    }
}