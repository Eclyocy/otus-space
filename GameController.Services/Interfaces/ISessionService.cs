namespace GameController.Services
{
    /// <summary>
    /// Interface for working with game sessions.
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Create new game session for user <paramref name="userId"/>.
        /// </summary>
        public void CreateUserSession(Guid userId);

        /// <summary>
        /// Get list of game sessions <see cref="Guid"/> for user <paramref name="userId"/>.
        /// </summary>
        public List<Guid> GetUserSessions(Guid userId);

        /// <summary>
        /// Get information on session <paramref name="sessionId"/>
        /// of user <paramref name="userId"/>.
        /// </summary>
        /// <remarks>
        /// TODO: information received from the ShipService: metrics and current turn.
        /// Currently it is a stub with an integer.
        /// </remarks>
        public int GetUserSessionInformation(Guid userId, Guid sessionId);
    }
}
