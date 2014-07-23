using Daycareinator.Data;
using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Services
{
    public interface IUsersService
    {
        User GetUserByUserName(string userName);
        IEnumerable<User> GetUsers(string userName);
        void Delete(int userId, int clientId);
    }

    public class UsersService : IUsersService
    {
        private IUnitOfWork _uow;
       
        public UsersService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public User GetUserByUserName(string userName)
        {
            return _uow.Users.FirstOrDefault(u => u.EmailAddress == userName);
        }

        public IEnumerable<User> GetUsers(string userName)
        {
            var user = GetUserByUserName(userName);
            return _uow.Users.Find(u => u.ClientId == user.ClientId);
        }

        public void Delete(int userId, int clientId)
        {
            var user = _uow.Users.FirstOrDefault(u => u.UserId == userId && u.ClientId == clientId);
            _uow.Users.Delete(user);
            _uow.Commit();
        }
    }
}
