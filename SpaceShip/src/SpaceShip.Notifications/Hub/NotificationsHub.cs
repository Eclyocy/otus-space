using Microsoft.AspNetCore.SignalR;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Notifications;

/// <summary>
/// Notifications Hub for spaceships notifications for user (UI or GameController).
/// </summary>
public class NotificationsHub : Hub
{
    private readonly IShipService _shipService;

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationsHub"/> class.
    /// </summary>
    /// <param name="shipService">Service which providing spaceship base operations.</param>
    public NotificationsHub(IShipService shipService)
        : base()
    {
        _shipService = shipService;
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
        if (!await ShipExist(shipId))
        {
            return;
        }

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
            if (!await ShipExist(id))
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

    private async Task<bool> ShipExist(Guid id)
    {
        return await _shipService.TryGetShipAsync(id);
    }

    #endregion
}
