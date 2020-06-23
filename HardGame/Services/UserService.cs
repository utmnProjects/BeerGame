using HardGame.Data;
using HardGame.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HardGame.Services
{
    public class UserService : IUserService
    {
        BeerHouseGameContext context;
        public UserService(BeerHouseGameContext context)
        {
            this.context = context;
        }

        public string Add(string userName, string connectId)
        {
            var user = new User()
            {
                Name = userName,
                Id = Guid.NewGuid().ToString(),
                ConnectId = connectId
            };

            context.User.Add(user);
            context.SaveChanges();
            return user.Id;
        }

        public User GetCurrentUser(string id)
        {
            return context.User.Include(x => x.Room).FirstOrDefault(x => x.Id == id);
        }

        public bool IsExistsUser(string name)
        {
            return context.User.FirstOrDefault(x => x.Name == name) != null;
        }

        public void Remove(string userId)
        {
            var currentUser = context.User.FirstOrDefault(x => x.Id == userId);
            if (currentUser == null)
                throw new ArgumentException("Пользователь не найден");
            else {
                context.User.Remove(currentUser);
                context.SaveChanges();
            }
        }
    }
}
