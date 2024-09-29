using GameController.Services.Helpers;
using GameController.Services.Interfaces;
using GameController.Services.Models.Generator;
using GameController.Services.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace GameController.Services.Services
{
    /// <summary>
    /// Service for working with generators.
    /// </summary>
    public class GeneratorService : IGeneratorService
    {
        #region private fields

        private readonly ILogger<GeneratorService> _logger;

        private readonly RestClient _restClient;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public GeneratorService(
            ILogger<GeneratorService> logger,
            IOptions<GeneratorApiSettings> options)
        {
            _logger = logger;

            _restClient = new RestClient(
                $"http://{options.Value.Hostname}:{options.Value.Port}/api/generators",
                configureSerialization: cfg => cfg.UseNewtonsoftJson(new JsonSerializerSettings()));
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public async Task<Guid> CreateGeneratorAsync(CreateGeneratorDto createGeneratorDto)
        {
            _logger.LogInformation("Create generator via request: {request}", createGeneratorDto);

            RestRequest request = new();
            request.AddJsonBody(createGeneratorDto);
            RestResponse<GeneratorDto> generatorResponse = await _restClient.ExecutePostAsync<GeneratorDto>(request);

            GeneratorDto generatorDto = RequestHelper.ValidateResponse(
                _restClient.BuildUri(request),
                generatorResponse,
                _logger);

            return generatorDto.Id;
        }

        #endregion
    }
}
