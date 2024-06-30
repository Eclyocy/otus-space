using GameController.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameController.Database.Interfaces
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        User GetUser(Guid userId);
     //   void UpdateUser(User user, Session session);
    }
}
