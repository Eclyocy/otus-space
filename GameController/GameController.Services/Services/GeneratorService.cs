using GameController.Services.Helpers;
using GameController.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GameController.Services.Services
{
    /// <summary>
    /// Service for working with generators.
    /// </summary>
    public class GeneratorService : IGeneratorService
    {
        #region private fields

        private readonly ILogger<GeneratorService> _logger;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public GeneratorService(
            ILogger<GeneratorService> logger)
        {
            _logger = logger;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public async Task<Guid> CreateGeneratorAsync()
        {
            _logger.LogInformation("Create generator");

            Guid generatorId = await GuidGenerator.GenerateGuidAsync();

            _logger.LogInformation("Created generator with ID {generatorId}", generatorId);

            return generatorId;
        }

        #endregion
    }
}
