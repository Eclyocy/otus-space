using GameController.Database.Interfaces;
using GameController.Database.Models;

namespace GameController.Database.Repositories
{
    /// <summary>
    /// UserRepository class.
    /// </summary>
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        #endregion
    }
}
