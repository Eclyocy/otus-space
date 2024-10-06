using GameController.API.Middleware;
using Microsoft.AspNetCore.Builder;

namespace WebAPI.ApplicationBuilderExtensions;

/// <summary>
/// Extensions for application builder.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Inject custom exception handler middleware.
    /// </summary>
    public static void UseCustomErrorHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
