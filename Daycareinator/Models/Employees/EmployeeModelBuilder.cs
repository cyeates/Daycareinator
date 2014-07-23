using AutoMapper;
using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Employees
{
    public class EmployeeModelBuilder
    {
        public EmployeeModel Build(Employee employee)
        {
            Mapper.CreateMap<Employee, EmployeeModel>();
            return Mapper.Map<Employee, EmployeeModel>(employee);
        }
    }
}