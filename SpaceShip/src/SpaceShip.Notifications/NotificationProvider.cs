using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SpaceShip.Notifications.Mappers;
using SpaceShip.Notifications.Models;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Notifications;

/// <summary>
/// Class provide sending notifications with spaceship metrics
/// Implement <see cref="INotificationsProvider"/> interface.
/// </summary>
public class NotificationsProvider : INotificationsProvider
{
    private readonly ILogger _logger;
    private readonly IHubContext<NotificationsHub> _hubContext;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationsProvider"/> class.
    /// </summary>
    /// <param name="logger">Inject logger.</param>
    /// <param name="hubContext">Inject hub context</param>
    public NotificationsProvider(
        ILogger<NotificationsProvider> logger,
        IHubContext<NotificationsHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;

        _mapper = new Mapper(
            new MapperConfiguration(static cfg =>
            {
                cfg.AddProfile<ShipMetricsProfile>();
            }));
    }

    /// <inheritdoc/>
    public async Task SendAsync(Guid shipId, SpaceShipDTO ship)
    {
        _logger.LogInformation("Sending notification to group {id} clients", shipId);
        await _hubContext.Clients.Group(shipId.ToString()).SendAsync(nameof(NotificationsProvider), _mapper.Map<SpaceShipMetricsNotification>(ship));
    }

    /// <inheritdoc/>
    public async Task SendAllAsync(SpaceShipDTO ship)
    {
        _logger.LogInformation("Sending notification to all clients");
        await _hubContext.Clients.All.SendAsync(nameof(NotificationsProvider), _mapper.Map<SpaceShipMetricsNotification>(ship));
    }

    /// <inheritdoc/>
    public async Task SubscribeAsync(string connectionId, IEnumerable<Guid> ids) // <-- кажется, что здесь нужен метод для пользователя, но это будет возможно после добавления авторизации
    {
        _logger.LogInformation("New request to subscribe from {connectionId} to ships {ships}", connectionId, ids);

        foreach (var id in ids)
        {
            await _hubContext.Groups.AddToGroupAsync(connectionId, id.ToString());
        }
    }
}
