using GameController.API.Middleware;

namespace GameController.API.ServicesExtensions
{
    /// <summary>
    /// Extensions for application builder.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        #region public methods

        /// <summary>
        /// Inject custom exception handler middleware.
        /// </summary>
        public static void UseCustomExceptionHandler(this IApplicationBuilder application)
        {
            application.UseMiddleware<ErrorHandlingMiddleware>();
        }

        #endregion
    }
}
