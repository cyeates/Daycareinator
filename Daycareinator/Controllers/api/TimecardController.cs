using Daycareinator.Data.Entities;
using Daycareinator.Domain;
using Daycareinator.Domain.Services;
using Daycareinator.Models.Timecards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Daycareinator.Controllers
{
    public class TimecardController : ApiController
    {
        private readonly IEmployeeTimecardService _timecardsService;
        private int _clientId;

        public TimecardController(IEmployeeTimecardService timecardsService, ICurrentUser currentUser)
        {
            _timecardsService = timecardsService;
            _clientId = currentUser.GetCurrentUser.ClientId;
        }

        [HttpGet]
        public TimecardsModel Get()
        {
            DateTime weekOf = DateTime.Now.AddDays(-7);
            var timecard = _timecardsService.GetTimecard(_clientId, weekOf);
            var builder = new TimecardsModelBuilder();
            var model = builder.Build(timecard, weekOf);

            return model;
        }

        [HttpPost]
        public TimecardsModel ChangeDate(DateModel date)
        {

            DateTime weekOf = date.Date;
            var timecard = _timecardsService.GetTimecard(_clientId, weekOf);
            var builder = new TimecardsModelBuilder();
            var model = builder.Build(timecard, weekOf);

            return model;
        }

        [HttpPost]
        public HttpResponseMessage Save(TimecardUpdateModel timecardModel)
        {

            var employees = new List<Employee>();
            foreach (var timecard in timecardModel.EmployeeTimecards)
            {
                employees.Add(timecard.ToEntity());
            }

            var result = _timecardsService.Save(employees, _clientId, timecardModel.Date.Date);
            return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, result);


        }

        
        //[ActionName("Submit")]
        [HttpPost]
        public HttpResponseMessage Submit(TimecardUpdateModel timecardModel)
        {
            var employees = new List<Employee>();
            foreach (var timecard in timecardModel.EmployeeTimecards)
            {
                employees.Add(timecard.ToEntity());
            }

            var result = _timecardsService.Save(employees, _clientId, timecardModel.Date.Date);
            if (!result.IsValid)
            {
                return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, ValidationResult.ErrorMessage("An error occurred while saving the time card.  The time card was not saved or submitted."));
            }
            
            result = _timecardsService.Submit(timecardModel.Date.Date, _clientId);
            if (!result.IsValid)
            {
                return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, ValidationResult.ErrorMessage("An error occurred while submitting the time card.  The time card was saved, but not submitted."));
            }

            return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, result);
        }
    }
}
