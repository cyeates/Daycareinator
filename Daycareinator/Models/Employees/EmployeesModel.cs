using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Employees
{
    public class EmployeesModel
    {
        public IEnumerable<EmployeeModel> Employees { get; set; }
    }
}