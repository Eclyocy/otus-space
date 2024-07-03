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
    }
}
