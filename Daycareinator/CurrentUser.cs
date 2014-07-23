using Daycareinator.Data.Entities;
using Daycareinator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Daycareinator
{
    public interface ICurrentUser
    {
        User GetCurrentUser {get;}
    }
    public class CurrentUser : ICurrentUser
    {
        private IIdentity _identity;
        private IUsersService _usersService;
        public CurrentUser(IUsersService usersService, IIdentity identity)
        {
            _usersService = usersService;
            _identity = identity;
        }

        public User GetCurrentUser
        {
            get
            {
                return _usersService.GetUserByUserName(_identity.Name);
            }
            
        }
    }
}