using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace GameController.Services.Hubs
{
    /// <summary>
    /// SignalR hub to handle user notifications.
    /// </summary>
    [Authorize]
    public class UserHub : Hub
    {
        #region public constants

        /// <summary>
        /// Send notification on user name update.
        /// </summary>
        public const string RefreshUserName = nameof(RefreshUserName);

        #endregion

        #region private constants

        private readonly ILogger _logger;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserHub"/> class.
        /// </summary>
        /// <param name="logger">Logger class from DI.</param>
        public UserHub(ILogger<UserHub> logger)
            : base()
        {
            _logger = logger;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public override async Task OnConnectedAsync()
        {
            _logger.LogDebug("Connection to hub [{HubClass}]. UserId: [{UserIdentifier}].", nameof(UserHub), Context.UserIdentifier);

            await base.OnConnectedAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogDebug("Disconnect user from hub [{HubClass}]. User: [{UserIdentifier}].", nameof(UserHub), Context.UserIdentifier);

            if (exception != null)
            {
                _logger.LogError(exception, "Disconnect exception message: {Message}", exception.Message);
            }

            return base.OnDisconnectedAsync(exception);
        }

        #endregion
    }
}
