using Microsoft.AspNetCore.SignalR;

namespace GameController.Services.Hubs
{
    /// <summary>
    /// SignalR hub to handle user notifications.
    /// </summary>
    public class UserHub : Hub
    {
        #region public constants

        /// <summary>
        /// Send notification on user name update.
        /// </summary>
        public const string RefreshUserName = nameof(RefreshUserName);

        #endregion
    }
}
