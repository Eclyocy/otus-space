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
    }
}
