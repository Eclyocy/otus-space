using Microsoft.AspNetCore.SignalR;
 
namespace SpaceShip.Notifications
{
    public class NotificationsHub : Hub
    {
        public async Task SubscribeToShip(Guid shipIds)
        {
            // TODO 
            // кажется, что тут нужна проверка на существование корабля, чтобы не плодить мертвые подписки, но насколко корректно размещать ее здесь?
            // хочется, чтобы зависимость была на уровень выше, а не в системном (наслдуемом от Hub) компоненте.

            await Groups.AddToGroupAsync(Context.ConnectionId, shipIds.ToString());
        }

        /// <summary>
        /// Just test method.
        /// Using to debug.
        /// </summary>
        /// /// <param name="message"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task SendAsync(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }
    }
}
