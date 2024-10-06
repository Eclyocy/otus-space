using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Notifications;

/// <summary>
/// Notifications Hub for spaceships notifications for user (UI or GameController).
/// </summary>
public class NotificationsHub : Hub
{
    #region private fields

    private readonly ISpaceShipService _shipService;

    private readonly ILogger<NotificationsHub> _logger;

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationsHub"/> class.
    /// </summary>
    /// <param name="shipService">Service which providing spaceship base operations.</param>
    /// <param name="loggerFactory">Logger factory.</param>
    public NotificationsHub(
        ISpaceShipService shipService,
        ILoggerFactory loggerFactory)
        : base()
    {
        _shipService = shipService;

        _logger = loggerFactory.CreateLogger<NotificationsHub>();
    }

    #endregion

    #region public methods

    /// <summary>
    /// Provide SignalR subscription to one spaceship metrics.
    /// </summary>
    /// <param name="shipId">Spaceship identifier. </param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    /// <remarks>Must be named as <see cref="NotificationMethod.Subscribe"/>.</remarks>
    public async Task Subscribe(Guid shipId)
    {
        _logger.LogInformation("Subscribing client {connectionId} to ship {shipId}.", Context.ConnectionId, shipId);

        if (!ShipExists(shipId))
        {
            _logger.LogInformation("Ship {shipId} does not exist, do not subscribe.", shipId);

            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, shipId.ToString());

        _logger.LogInformation("Client {connectionId} subscribed to ship {shipId}.", Context.ConnectionId, shipId);
    }

    /// <summary>
    /// Unsubscribe client from the group of subscribers for SignalR notifications on a specific spaceship.
    /// </summary>
    /// <param name="shipId">Spaceship identifier. </param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    /// <remarks>Must be named as <see cref="NotificationMethod.Unsubscribe"/>.</remarks>
    public async Task Unsubscribe(Guid shipId)
    {
        _logger.LogInformation(
            "Unsubscribing client {connectionId} from ship {shipId}.",
            Context.ConnectionId,
            shipId);

        if (!ShipExists(shipId))
        {
            _logger.LogInformation("Ship {shipId} does not exist, do not unsubscribe.", shipId);

            return;
        }

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, shipId.ToString());

        _logger.LogInformation("Client {connectionId} unsubscribed from ship {shipId}.", Context.ConnectionId, shipId);
    }

    /// <summary>
    /// Provide SignalR subscription to one spaceships.
    /// </summary>
    /// <param name="shipIds">List of spaceships identifiers.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public async Task SubscribeMany(List<Guid> shipIds)
    {
        foreach (var id in shipIds)
        {
            if (!ShipExists(id))
            {
                continue;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());
        }
    }

    /// <summary>
    /// Just test method.
    /// Using to debug.
    /// </summary>
    /// /// <param name="message">incoming message from client. </param>
    [Obsolete]
    public async Task SendAsync(string message)
    {
        await Clients.All.SendAsync(NotificationMethod.Refresh, message);
    }

    #endregion

    #region private methods

    private bool ShipExists(Guid id)
    {
        return _shipService.GetShip(id) != null;
    }

    #endregion
}
