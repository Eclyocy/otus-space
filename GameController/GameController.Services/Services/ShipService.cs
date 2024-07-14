using GameController.Services.Interfaces;
using GameController.Services.Models.Ship;
using GameController.Services.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace GameController.Services.Services
{
    /// <summary>
    /// Class for working with space ships.
    /// </summary>
    public class ShipService : IShipService
    {
        #region private fields

        private readonly ILogger<ShipService> _logger;

        private readonly RestClient _restClient;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ShipService(
            ILogger<ShipService> logger,
            IOptions<SpaceShipApiSettings> options)
        {
            _logger = logger;

            _restClient = new RestClient(
                $"http://{options.Value.Hostname}:{options.Value.Port}/api/v1/spaceships",
                configureSerialization: cfg => cfg.UseNewtonsoftJson(new JsonSerializerSettings()));
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public async Task<Guid> CreateShipAsync()
        {
            _logger.LogInformation("Create space ship");

            RestRequest request = new();
            RestResponse<ShipDto> shipResponse = await _restClient.ExecutePostAsync<ShipDto>(request);

            if (!shipResponse.IsSuccessful)
            {
                _logger.LogError(
                    "Unable to send request for space ship creation:\n{errorException}\n{errorMessage}",
                    shipResponse.ErrorException,
                    shipResponse.ErrorMessage);

                throw new Exception("Unable to send request for space ship creation.");
            }

            if (shipResponse.Data == null)
            {
                _logger.LogError("Unable to retrieve ship model.");

                throw new Exception("Unable to retrieve ship model.");
            }

            _logger.LogInformation("Created space ship with ID {shipId}", shipResponse.Data.Id);

            return shipResponse.Data.Id;
        }

        /// <inheritdoc/>
        public async Task<ShipDto> GetShipAsync(Guid shipId)
        {
            _logger.LogInformation("Get space ship {shipId}", shipId);

            RestRequest request = new("/{shipId}");
            request.AddUrlSegment("shipId", shipId);
            RestResponse<ShipDto> shipResponse = await _restClient.ExecuteGetAsync<ShipDto>(request);

            if (!shipResponse.IsSuccessful)
            {
                _logger.LogError(
                    "Unable to send request for space ship retrieval:\n{errorException}\n{errorMessage}",
                    shipResponse.ErrorException,
                    shipResponse.ErrorMessage);

                throw new Exception("Unable to send request for space ship creation.");
            }

            if (shipResponse.Data == null)
            {
                _logger.LogError("Unable to retrieve ship model.");

                throw new Exception("Unable to retrieve ship model.");
            }

            return shipResponse.Data;
        }

        #endregion
    }
}
