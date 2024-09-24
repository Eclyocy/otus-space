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

    private readonly IShipService _shipService;

    private readonly ILogger<NotificationsHub> _logger;

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationsHub"/> class.
    /// </summary>
    /// <param name="shipService">Service which providing spaceship base operations.</param>
    /// <param name="loggerFactory">Logger factory.</param>
    public NotificationsHub(
        IShipService shipService,
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
    public async Task Subscribe(Guid shipId)
    {
        _logger.LogInformation("Subscribing to ship {shipId}.", shipId);

        if (!ShipExists(shipId))
        {
            _logger.LogInformation("Ship {shipId} does not exist, do not subscribe.", shipId);

            return;
        }

        _logger.LogInformation("Subscribing client {connectionId} to {shipId}.", Context.ConnectionId, shipId);

        await Groups.AddToGroupAsync(Context.ConnectionId, shipId.ToString());
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
        await Clients.All.SendAsync("Receive", message);
    }

    #endregion

    #region private methods

    private bool ShipExists(Guid id)
    {
        return _shipService.GetShip(id) != null;
    }

    #endregion
}
