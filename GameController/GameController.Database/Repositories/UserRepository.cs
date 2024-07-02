using GameController.Database.Interfaces;
using GameController.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace GameController.Database.Repositories
{
    public class UserRepository: IUserRepository
    {
        public UserRepository() 
        {
        }

       public User CreateUser(User user)
        {
            using (var dbContext = new SessionDbContext())
            {
                    var res = dbContext.users.Add(user);
                    dbContext.SaveChanges();
                    return res.Entity;
            }
        }

        public User GetUser(Guid userId)
        {  

            using (var dbContext = new SessionDbContext())
            {
                var user =  dbContext.users
                    .Include(u => u.Sessions)
                    .FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    throw new ArgumentException($"Пользователь с ID {userId} не найден."); 
                }

                return (user);
            }
        }

        public User UpdateUser(User user) 
        {
            using (var dbContext = new SessionDbContext())
            {
                // Обновляем пользователя
                var res = dbContext.users.Update(user);

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
        //crud
        //получение сессий пользователя ???
    }
}
