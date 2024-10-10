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

        #region public methods

        /// <inheritdoc/>
        public User? GetUserByLogin(string name)
        {
            return Context.Set<User>().Where(x => x.Name == name).FirstOrDefault(); // нужно будет изменить на SingleOrDefault как сделаю проверку на одинаковые логины
        }

        #endregion
    }
}
