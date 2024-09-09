namespace SpaceShip.Notifications;

public interface INotificationsProvider
{
    public Task SendAsync(string connectionId, object notification);

    public Task SendAllAsync(object notification);

    public Task SubscribeAsync(string clientId, IEnumerable<Guid> ids);
}
