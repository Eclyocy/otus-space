using System.Net;
using GameController.Services.Exceptions;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace GameController.Services.Helpers
{
    /// <summary>
    /// Helper for REST requests.
    /// </summary>
    public class RequestHelper
    {
        #region public methods

        /// <summary>
        /// Validate REST response and return its contents if successful.
        /// </summary>
        /// <typeparam name="T">Type of the resulting object.</typeparam>
        public static T ValidateResponse<T>(Uri uri, RestResponse<T> response, ILogger logger)
        {
            if (response.IsSuccessful)
            {
                if (response.Data != null)
                {
                    return response.Data;
                }

                logger.LogError("Unable to retrieve {modelType} model via request {uri}", typeof(T), uri);

                throw new PreconditionFailedException($"Unable to retrieve {typeof(T).Name} model.");
            }

            logger.LogError(
                "Unsuccessful request to {uri}: {errorStatusCode} {errorMessage}\n{errorException}",
                uri,
                response.StatusCode,
                response.ErrorMessage,
                response.ErrorException);

            throw response.StatusCode switch
            {
                HttpStatusCode.NotFound =>
                    new NotFoundException($"Remote server responded with {typeof(T).Name} not found"),
                HttpStatusCode.Conflict when response.ErrorMessage is not null =>
                    new ConflictException(response.ErrorMessage),
                _ =>
                    new Exception("Remote server responded with an error."),
            };
        }

        #endregion
    }
}
