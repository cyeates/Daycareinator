using Daycareinator.Data;
using Daycareinator.Data.Entities;
using Daycareinator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Daycareinator.Controllers
{
    public class BaseController : Controller
    {
        private IUsersService _usersService;
       
        
        public BaseController(IUsersService usersService)
        {
            _usersService = usersService;
            
        }

        public BaseController()
        {

        }

        public User CurrentUser
        {
            get 
            {
                return _usersService.GetUserByUserName(User.Identity.Name);
            }
            
        }

        

    }
}
