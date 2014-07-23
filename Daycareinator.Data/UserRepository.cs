using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Data
{
    public interface IUserRepository
    {
        User GetUserByUserName(string userName);
        IEnumerable<User> GetUsersByClient(int clientId);
    }
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        private DaycareinatorContext _context;

        public UserRepository(DaycareinatorContext context) : base(context)
        {
            _context = context;
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.EmailAddress.ToLower() == userName.ToLower());
        }

        public IEnumerable<User> GetUsersByClient(int clientId)
        {
            return base.Find(c => c.ClientId == clientId);
        }
    }
}
