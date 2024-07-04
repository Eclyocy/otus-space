using GameController.Database.Interfaces;
using GameController.Database.Models;

namespace GameController.Database.Repositories
{
    /// <summary>
    /// Session repository.
    /// </summary>
    public class SessionRepository : ISessionRepository
    {
        #region public methods

        /// <inheritdoc/>
        public Session Create(Session entity)
        {
            return new();
        }

        /// <inheritdoc/>
        public Session? Get(Guid id)
        {
            return new();
        }

        /// <inheritdoc/>
        public List<Session> GetAllByUserId(Guid userId)
        {
            return new();
        }

        /// <inheritdoc/>
        public Session Update(Session entity)
        {
            return new();
        }

        /// <inheritdoc/>
        public bool Delete(Guid id)
        {
            return true;
        }

        #endregion
    }
}
