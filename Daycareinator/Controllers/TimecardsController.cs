using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daycareinator.Domain.Services;
using Daycareinator.Models.Timecards;

namespace Daycareinator.Controllers
{
    [Authorize]
    public class TimecardsController : Controller
    {
        private readonly IEmployeeTimecardService _timecardsService;
        private int _clientId;
        private ICurrentUser _currentUser;

        public TimecardsController(IEmployeeTimecardService timecardsService, ICurrentUser currentUser)
        {
            _timecardsService = timecardsService;
            _currentUser = currentUser;
            //_clientId = _currentUser.GetCurrentUser.ClientId;
        }


        public ActionResult Index()
        {
           return View();
        }

        [HttpPost]
        public JsonResult Save(IEnumerable<EmployeeTimecardUpdateModel> timecards)
        {

            return Json("success");
            //var employees = new List<Employee>();
            //foreach (var timecard in timecards)
            //{
            //    employees.Add(timecard.ToEntity());
            //}

            //var result = _timecardsService.Save(employees, _clientId);
            //return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, result);


        }

    }
}
