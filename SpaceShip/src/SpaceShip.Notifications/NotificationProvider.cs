

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace SpaceShip.Notifications;

public class NotificationsProvider : INotificationsProvider
{
    private readonly ILogger _logger;
    private readonly IHubContext<NotificationsHub> _hubContext;

    public NotificationsProvider(
        ILogger<NotificationsProvider> logger,
        IHubContext<NotificationsHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
    }

    public async Task SendAsync(string connectionId, object notification)
    {
        await _hubContext.Clients.Client(connectionId).SendAsync(nameof(NotificationsProvider), notification);
    }

    public async Task SendAllAsync(object notification)
    {
        await _hubContext.Clients.All.SendAsync(nameof(NotificationsProvider), notification);
    }

    public async Task SubscribeAsync(string clientId, IEnumerable<Guid> ids)
    {
        _logger.LogInformation("New request to subscribe from client {clientId} to sips {sips}", clientId, ids);
        
        // TODO - check if ship is exist and add to mapping dict.

    }
}
