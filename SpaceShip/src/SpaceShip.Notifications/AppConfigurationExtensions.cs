using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace SpaceShip.Notifications;

public static class AppConfigurationExtensions
{
    public static void UseNotifications(this IEndpointRouteBuilder app, string url= "/notificationsHub" )
    {
        app.MapHub<NotificationsHub>(url);
    }
}
