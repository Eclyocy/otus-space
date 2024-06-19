
namespace SessionController.Services
{
    /// <summary>
    /// Класс для работы с игровыми сессиями.
    /// </summary>
    public class SessionService : ISessionService
    {
        #region private fields

        private readonly ILogger<SessionService> _logger;

        #endregion

        #region constructor

        public SessionService(ILogger<SessionService> logger)
        {
            _logger = logger;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public void CreateUserSession(Guid userId)
        {
            _logger.LogInformation("Create session for user {userId}.", userId);
        }

        /// <inheritdoc/>
        public List<Guid> GetUserSessions(Guid userId)
        {
            _logger.LogInformation("Get sessions of user {userId}.", userId);

            return new();
        }

        /// <inheritdoc/>
        public int GetUserSessionInformation(Guid userId, Guid sessionId)
        {
            _logger.LogInformation("Get information on session {sessionId} of user {userId}.",
                sessionId,
                userId);

            return new();
        }

        #endregion
    }
}
