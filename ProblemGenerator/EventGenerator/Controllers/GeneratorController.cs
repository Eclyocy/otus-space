using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Generator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventGenerator.API.Controllers
{
    /// <summary>
    /// Controller for actions with generators.
    /// </summary>
    [ApiController]
    [Route("/api/generator")]
    public class GeneratorController : Controller
    {
        private readonly IGeneratorService _generatorService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GeneratorController(IGeneratorService service, IMapper mapper)
        {
            _generatorService = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Заглушка - Получение Guid корабля.
        /// </summary>
        [HttpGet]
        [SwaggerOperation("Получение Guid корабля - заглушка")]
        public CreateGeneratorRequest GetShip()
        {
            return new CreateGeneratorRequest()
            {
                ShipId = Guid.NewGuid()
            };
        }

        /// <summary>
        /// Создать новый генератор событий.
        /// </summary>
        [HttpPost]
        [Route("{shipId}")]
        [SwaggerOperation("Создание нового генератора событий")]
        public GeneratorResponse CreateGenerator(CreateGeneratorRequest createGeneratorRequest)
        {
            CreateGeneratorDto createGeneratorDto = _mapper.Map<CreateGeneratorDto>(createGeneratorRequest);
            GeneratorDto generatorDto = _generatorService.CreateGenerator(createGeneratorDto);
            return _mapper.Map<GeneratorResponse>(generatorDto);
        }

        /// <summary>
        /// Создать новое событие.
        /// </summary>
        [HttpGet]
        [Route("{shipId}")]
        [SwaggerOperation("Получение существующего генератора событий")]
        public GeneratorResponse GetGenerator(Guid shipId)
        {
            GeneratorDto generatorDto = _generatorService.GetGenerator(shipId);
            return _mapper.Map<GeneratorResponse>(generatorDto);
        }
    }
}
