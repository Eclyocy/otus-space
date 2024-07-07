using GameController.Services.Helpers;
using GameController.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GameController.Services.Services
{
    /// <summary>
    /// Class for working with space ships.
    /// </summary>
    public class ShipService : IShipService
    {
        #region private fields

        private readonly ILogger<ShipService> _logger;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ShipService(
            ILogger<ShipService> logger)
        {
            _logger = logger;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public async Task<Guid> CreateShipAsync()
        {
            _logger.LogInformation("Create space ship");

            Guid shipId = await GuidGenerator.GenerateGuidAsync();

            _logger.LogInformation("Created space ship with ID {shipId}", shipId);

            return shipId;
        }

        #endregion
    }
}
