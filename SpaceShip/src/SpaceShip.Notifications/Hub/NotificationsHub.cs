using Microsoft.AspNetCore.SignalR;
 
namespace SpaceShip.Notifications
{
    public class NotificationsHub : Hub
    {
        public async Task SubscribeToShip(Guid shipId)
        {
            Guid tmp = shipId;
            var clientId = Context.ConnectionId;
            var user = Context.User;
        }

        /// <summary>
        /// Just test method.
        /// Using to debug.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task SendAsync(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }
    }
}
