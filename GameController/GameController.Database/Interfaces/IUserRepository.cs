using GameController.Database.Models;

namespace GameController.Database.Interfaces
{
    /// <summary>
    /// Repository for users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// CreateUser.
        /// </summary>
        User CreateUser(User user);

        /// <summary>
        /// GetUser.
        /// </summary>
        User GetUser(Guid userId);

        /// <summary>
        /// UpdateUser.
        /// </summary>
        User UpdateUser(User user);
    }
}
