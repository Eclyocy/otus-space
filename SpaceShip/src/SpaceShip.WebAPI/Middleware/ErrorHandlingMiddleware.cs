using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using SpaceShip.Services.Exceptions;

namespace SpaceShip.WebAPI.Middleware
{
    /// <summary>
    /// Middleware for converting unhandled exceptions into error response.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        #region private fields

        private readonly RequestDelegate _next;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Try converting exception into error response.
        /// </summary>
        public async Task InvokeAsync(HttpContext context, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                await WriteDevelopmentResponse(context);
            }
            else
            {
                await WriteProductionResponse(context);
            }
        }

        #endregion

        #region private methods

        private Task WriteDevelopmentResponse(HttpContext httpContext)
            => WriteResponse(httpContext, includeDetails: true);

        private Task WriteProductionResponse(HttpContext httpContext)
            => WriteResponse(httpContext, includeDetails: false);

        private async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            IExceptionHandlerFeature? exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();

            if (exceptionDetails == null)
            {
                await _next(httpContext);

                return;
            }

            Exception errorException = exceptionDetails.Error;

            int statusCode = errorException is BaseException baseException
                ? (int)baseException.StatusCode
                : (int)HttpStatusCode.InternalServerError;

            object problem = new
            {
                status = statusCode,
                message = errorException.Message,
                details = includeDetails ? errorException.ToString() : null
            };

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = statusCode;

            using Stream stream = httpContext.Response.Body;

            await JsonSerializer.SerializeAsync(stream, problem);
        }

        #endregion
    }
}
