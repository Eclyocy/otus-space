using GameController.Database.Models;

namespace GameController.Database.Interfaces
{
    /// <summary>
    /// Interface for <see cref="Session"/> repository.
    /// </summary>
    public interface ISessionRepository : IRepository<Session>
    {
        /// <summary>
        /// Get all sessions of a specific user.
        /// </summary>
        List<Session> GetAllByUserId(Guid userId);
    }
}
