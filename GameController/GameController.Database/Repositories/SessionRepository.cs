using GameController.Database.Interfaces;
using GameController.Database.Models;

namespace GameController.Database.Repositories
{
    /// <summary>
    /// Session repository.
    /// </summary>
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SessionRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public List<Session> GetAllByUserId(Guid userId)
        {
            return Context.Set<Session>()
                .Where(x => x.UserId == userId)
                .ToList();
        }

        #endregion
    }
}
