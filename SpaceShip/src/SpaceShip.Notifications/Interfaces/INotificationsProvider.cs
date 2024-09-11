namespace SpaceShip.Notifications;

public interface INotificationsProvider
{
    public Task SendAsync(Guid shipId, object notification);

    public Task SendAllAsync(object notification);

    public Task SubscribeAsync(string clientId, IEnumerable<Guid> ids);
}
