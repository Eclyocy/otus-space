using GameController.Database.Interfaces;
using GameController.Database.Models;

namespace GameController.Database.Repositories
{
    /// <summary>
    /// Session repository.
    /// </summary>
    internal class SessionRepository : BaseRepository<Session>, ISessionRepository
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
        public List<Session> GetAll(Guid userId)
        {
            var res = Context.Set<Session>()
                .Where(x => x.UserId == userId)
                .ToList();
            return res;
        }

        #endregion
    }
}
