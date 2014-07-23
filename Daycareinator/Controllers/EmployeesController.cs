using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Daycareinator.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
    }
}
