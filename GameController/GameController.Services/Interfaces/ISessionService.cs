using GameController.Services.Models.Session;

namespace GameController.Services.Interfaces
{
    /// <summary>
    /// Interface for working with game sessions.
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Create a game session.
        /// </summary>
        Task<SessionDto> CreateUserSessionAsync(Guid userId);

        /// <summary>
        /// Get list of game sessions of user <paramref name="userId"/>.
        /// </summary>
        List<SessionDto> GetUserSessions(Guid userId);

        /// <summary>
        /// Get information on game session <paramref name="sessionId"/>
        /// of user <paramref name="userId"/>.
        /// </summary>
        SessionDto GetUserSession(Guid userId, Guid sessionId);

        /// <summary>
        /// Make move in game session <paramref name="sessionId"/>
        /// of user <paramref name="userId"/>.
        /// </summary>
        void MakeMove(Guid userId, Guid sessionId);
    }
}
