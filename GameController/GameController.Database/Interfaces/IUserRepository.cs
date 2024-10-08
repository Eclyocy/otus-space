using GameController.Database.Models;

namespace GameController.Database.Interfaces
{
    /// <summary>
    /// Interface for <see cref="User"/> repository.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Method for getting user by name.
        /// </summary>
        public User? GetUserByLogin(string name);
    }
}
