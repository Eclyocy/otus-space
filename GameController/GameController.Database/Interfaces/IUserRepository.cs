using GameController.Database.Models;

namespace GameController.Database.Interfaces
{
    /// <summary>
    /// Interface for <see cref="User"/> repository.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Get user by <see cref="User.Name"/>.
        /// </summary>
        User? GetUserByName(string name);
    }
}
