using GameController.Database.Models;

namespace GameController.Database.Interfaces
{
    /// <summary>
    /// Interface for session repository.
    /// </summary>
    public interface ISessionRepository : IRepository<Session>
    {
        /// <summary>
        /// Get all user sessions. <paramref name="userId"/>.
        /// </summary>
        List<Session> GetAll(Guid userId);
    }
}
