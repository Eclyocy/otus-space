using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using EventGenerator.Services.Models.Generator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventGenerator.API.Controllers
{
    /// <summary>
    /// Controller for actions with generators.
    /// </summary>
    [ApiController]
    [Route("/api/generators")]
    public class GeneratorController : Controller
    {
        private readonly IGeneratorService _generatorService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GeneratorController(IGeneratorService generatorService, IMapper mapper)
        {
            _generatorService = generatorService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create new event generator.
        /// </summary>
        [HttpPost]
        [Route("")]
        [SwaggerOperation("Создание нового генератора событий")]
        public GeneratorResponse CreateGenerator(CreateGeneratorRequest createGeneratorRequest)
        {
            CreateGeneratorDto createGeneratorDto = _mapper.Map<CreateGeneratorDto>(createGeneratorRequest);
            GeneratorDto generatorDto = _generatorService.CreateGenerator(createGeneratorDto);
            return _mapper.Map<GeneratorResponse>(generatorDto);
        }

        /// <summary>
        /// Get existing event generator.
        /// </summary>
        [HttpGet]
        [Route("{generatorId}")]
        [SwaggerOperation("Получение существующего генератора событий")]
        public GeneratorResponse GetGenerator(Guid generatorId)
        {
            GeneratorDto generatorDto = _generatorService.GetGenerator(generatorId);
            return _mapper.Map<GeneratorResponse>(generatorDto);
        }

        /// <summary>
        /// Add a trouble coin to event generator.
        /// </summary>
        [HttpPost]
        [Route("{generatorId}/coins")]
        [SwaggerOperation("Добавить генератору событий монетку")]
        public GeneratorResponse AddTroubleCoin(Guid generatorId)
        {
            GeneratorDto generatorDto = _generatorService.AddTroubleCoin(generatorId);
            return _mapper.Map<GeneratorResponse>(generatorDto);
        }

        /// <summary>
        /// Get events of a specific generator.
        /// </summary>
        [HttpGet]
        [Route("{generatorId}/events")]
        [SwaggerOperation("Получение всех событий, сгенерированных существующим генератором")]
        public List<EventResponse> GetEvents(Guid generatorId)
        {
            List<EventDto> eventDtos = _generatorService.GetEvents(generatorId);
            return _mapper.Map<List<EventResponse>>(eventDtos);
        }

        /// <summary>
        /// Create new event.
        /// </summary>
        [HttpPost]
        [Route("{generatorId}/events")]
        [SwaggerOperation("Создание нового события")]
        public EventResponse CreateEvent(Guid generatorId)
        {
            EventDto eventDto = _generatorService.GenerateEvent(generatorId);
            return _mapper.Map<EventResponse>(eventDto);
        }
    }
}
