using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGenerator.Services.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly ILogger<GeneratorService> _logger;

        public GeneratorService(
            ILogger<GeneratorService> logger)
        {
            _logger = logger;
        }

        public async Task<Guid> CreateGeneratorAsync()
        {
            _logger.LogInformation("Create generator");

            Guid generatorId = await GuidGenerator.GenerateGuidAsync();

            _logger.LogInformation("Created generator with ID {generatorId}", generatorId);

            return generatorId;
        }
    }
}