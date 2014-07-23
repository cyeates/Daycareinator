using Daycareinator.Data;
using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Validation
{
    public class UserAccessToEmployeeValidator
    {
        private IUnitOfWork _uow;
        public UserAccessToEmployeeValidator(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public bool Validate(User user, int employeeId)
        {
            var employee = _uow.Employees.GetById(employeeId);
            return employee.ClientId == user.ClientId;
        }
    }
}
