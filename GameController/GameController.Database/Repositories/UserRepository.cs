using GameController.Database.Interfaces;
using GameController.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace GameController.Database.Repositories
{
    /// <summary>
    /// UserRepository class.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// constructor.
        /// </summary>
        public UserRepository()
        {
        }

        /// <inheritdoc/>
        public User CreateUser(User user)
        {
            using (var dbContext = new SessionDbContext())
            {
                var res = dbContext.Users.Add(user);
                dbContext.SaveChanges();
                return res.Entity;
            }
        }

        /// <inheritdoc/>
        public User GetUser(Guid userId)
        {
            using (var dbContext = new SessionDbContext())
            {
                var user = dbContext.Users
                    .Include(u => u.Sessions)
                    .FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    throw new ArgumentException($"Пользователь с ID {userId} не найден.");
                }

                return user;
            }
        }

        /// <inheritdoc/>
        public User UpdateUser(User user)
        {
            using (var dbContext = new SessionDbContext())
            {
                // Обновляем пользователя
                var res = dbContext.Users.Update(user);

                // Сохраняем изменения
                dbContext.SaveChanges();
                return res.Entity;
            }
        }

      /*  public void DeleteUser(int userId, int sessionId)
        {
            using (var dbContext = new SessionDbContext())
            {
                // Получаем пользователя и сессию по ID
                var user =  dbContext.users.FindAsync(userId);
                var session = dbContext.sessions.FindAsync(sessionId);

                // Удаляем пользователя и сессию
                dbContext.Users.Remove(user);
                dbContext.Sessions.Remove(session);

                // Сохраняем изменения
                await dbContext.SaveChangesAsync();
            }
        }*/

        // crud
        // получение сессий пользователя ???
    }
}
