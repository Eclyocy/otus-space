using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Generator;
using Microsoft.Extensions.Logging;

namespace EventGenerator.Services.Services
{
    /// <summary>
    /// Class for working with event generator servisce.
    /// </summary>
    public class GeneratorService : IGeneratorService
    {
        /// <summary>
        /// Const.
        /// </summary>
        private const int TroublecointStart = 10;

        private readonly IGeneratorRepository _generatorRepository;
        private readonly ILogger<GeneratorService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GeneratorService(IGeneratorRepository generatorRepository, ILogger<GeneratorService> logger, IMapper mapper)
        {
            _generatorRepository = generatorRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public GeneratorDto CreateGenerator(CreateGeneratorDto createGeneratorDto)
        {
            _logger.LogInformation("Create generator by request {createGeneratorDto}", createGeneratorDto);

            Generator generatorRequest = _mapper.Map<Generator>(createGeneratorDto);
            generatorRequest.Troublecoint = TroublecointStart;
            Generator generator = _generatorRepository.Create(generatorRequest);

            return _mapper.Map<GeneratorDto>(generator);
        }

        /// <inheritdoc/>
        public GeneratorDto GetGenerator(Guid shipId)
        {
            _logger.LogInformation("Get generator by ship ID {shipId}", shipId);

            Generator generator = _generatorRepository.Get(shipId);

            return _mapper.Map<GeneratorDto>(generator);
        }
    }
}
