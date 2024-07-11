using GameController.Database.Interfaces;
using GameController.Database.Models;

namespace GameController.Database.Repositories
{
    /// <summary>
    /// User repository.
    /// </summary>
    public class UserRepository : BaseRepository<User>, IUserRepository
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
