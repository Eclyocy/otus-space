using GameController.Database.Interfaces;
using GameController.Database.Models;

namespace GameController.Database.Repositories
{
    /// <summary>
    /// User repository.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        #region public methods

        /// <inheritdoc/>
        public User Create(User entity)
        {
            return new();
        }

        /// <inheritdoc/>
        public User? Get(Guid id)
        {
            return new();
        }

        /// <inheritdoc/>
        public User Update(Guid id, User entity)
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
