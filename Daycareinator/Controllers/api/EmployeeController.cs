using AutoMapper;
using Daycareinator.Data.Entities;
using Daycareinator.Domain;
using Daycareinator.Domain.Services;
using Daycareinator.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Daycareinator.Controllers.api
{
    [Authorize]
    public class EmployeeController : ApiController
    {
       
        private IEmployeesService _employeesService;
        private ICurrentUser _currentUser;
        public EmployeeController(ICurrentUser currentUser, IEmployeesService employeesService)
        {
           
            _currentUser = currentUser;
            _employeesService = employeesService;
        }

        [HttpGet]
        public EmployeesModel Get()
        {
            var employees = _employeesService.GetEmployees(_currentUser.GetCurrentUser.ClientId).ToList();

            var builder = new EmployeeModelBuilder();

            var employeeModels = new List<EmployeeModel>();
            employees.OrderBy(e => e.FirstName)
                     .ToList()
                     .ForEach(e => employeeModels.Add(builder.Build(e)));
                     
                     
            var model = new EmployeesModel { Employees = employeeModels };
            return model;


        }

       
        public HttpResponseMessage Put(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<EmployeeModel, Employee>();
                var employeeDto = Mapper.Map<EmployeeModel, Employee>(employee);
                _employeesService.Update(employeeDto, _currentUser.GetCurrentUser.ClientId);
            }


            var result = ValidationResult.SuccessMessage(string.Format("{0} {1} was saved successfully.", employee.FirstName, employee.LastName));
            return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, result);

        }

        
        public HttpResponseMessage Post(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<EmployeeModel, Employee>();
                var employeeDto = Mapper.Map<EmployeeModel, Employee>(employee);
                _employeesService.Add(employeeDto, _currentUser.GetCurrentUser.ClientId);
            }


            var result = ValidationResult.SuccessMessage(string.Format("{0} {1} was saved successfully.", employee.FirstName, employee.LastName));
            return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, result);

        }


        public HttpResponseMessage Delete(int id)
        {

            _employeesService.Delete(id, _currentUser.GetCurrentUser.ClientId);
            var result = ValidationResult.SuccessMessage("The employee was deleted successfully.");
            return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, result);


        }
    }
}
