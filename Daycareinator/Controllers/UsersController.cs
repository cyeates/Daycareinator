using Daycareinator.Data;
using Daycareinator.Domain;
using Daycareinator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Daycareinator.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private IUsersService _usersService;
        private IInvitationService _invitationService;
        private ICurrentUser _currentUser;

        public UsersController(IUsersService usersService, IInvitationService invitationService, ICurrentUser currentUser)
        {
            _usersService = usersService;
            _invitationService = invitationService;
            _currentUser = currentUser;
        }

        public ActionResult Index()
        {
            var users = _usersService.GetUsers(User.Identity.Name);
            return View();
        }



    }
}
