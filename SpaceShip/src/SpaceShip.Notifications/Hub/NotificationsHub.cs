using Microsoft.AspNetCore.SignalR;
 
namespace SpaceShip.Notifications
{
    public class NotificationsHub : Hub, INotificationsHub
    {
        public async Task SendAsync(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }
    }
}
