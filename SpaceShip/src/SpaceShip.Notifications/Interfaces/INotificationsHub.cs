namespace SpaceShip.Notifications;

public interface INotificationsHub
{
    public Task SendAsync(string msg);
}
