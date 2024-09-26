using SpaceShip.Service.Contracts;

namespace SpaceShip.Notifications;

/// <summary>
/// Interface provide sending notifications with spaceship metrics.
/// </summary>
public interface INotificationsProvider
{
    /// <summary>
    /// Send metrics for one ships subscribers.
    /// </summary>
    /// <param name="shipId">Spaceship identifiers</param>
    /// <param name="shipDTO">DTO with spaceship metrics from service layer.</param>
    public Task SendAsync(Guid shipId, SpaceShipDTO shipDTO);

    /// <summary>
    /// Send metrics for all subscribers.
    /// </summary>
    /// <param name="shipDTO">DTO with spaceship metrics from service layer.</param>
    public Task SendAllAsync(SpaceShipDTO shipDTO);

    /// <summary>
    /// Provide subscription for ships by clientId
    /// </summary>
    /// <param name="clientId">SignalR client identifier</param>
    /// <param name="ids">List of spaceship identifiers</param>
    public Task SubscribeAsync(string clientId, IEnumerable<Guid> ids);
}
