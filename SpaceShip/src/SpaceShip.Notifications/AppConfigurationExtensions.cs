using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace SpaceShip.Notifications;

/// <summary>
/// Extension for application for registration hub.
/// </summary>
public static class AppConfigurationExtensions
{
    /// <summary>
    /// Extension method to register SignalR hub.
    /// </summary>
    /// <param name="app">Application.</param>
    /// <param name="url">Endpoint for SignalR notifications methods.</param>
    public static void UseNotifications(this IEndpointRouteBuilder app, string url = "/notifications-hub")
    {
        app.MapHub<NotificationsHub>(url);
    }
}
