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

    public async Task SendAsync(Guid shipId, object notification)
    {
        _logger.LogInformation("Sending notification to group {id} clients", shipId);
        await _hubContext.Clients.Group(shipId.ToString()).SendAsync(nameof(NotificationsProvider), notification);
    }

    public async Task SendAllAsync(object notification)
    {
        _logger.LogInformation("Sending notification to all clients");
        await _hubContext.Clients.All.SendAsync(nameof(NotificationsProvider), notification);
    }

    public async Task SubscribeAsync(string connectionId, IEnumerable<Guid> ids) // <-- кажется, что здесь нужен метод для пользователя, но это будет возможно после добавления авторизации
    {
        _logger.LogInformation("New request to subscribe from {connectionId} to ships {ships}", connectionId, ids);
        
        foreach(var id in ids)
        {
            await _hubContext.Groups.AddToGroupAsync(connectionId, id.ToString());
        }

    }
}
