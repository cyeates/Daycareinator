using Daycareinator.Data;
using Daycareinator.Data.Entities;
using EfficientlyLazy.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Services
{
    public interface IEmployeesService
    {
        IEnumerable<Employee> GetEmployees(int clientId);
        Employee Add(Employee dto, int clientId);
        Employee Update(Employee dto, int clientId);
        void Delete(int employeeId, int clientId);
    }
    public class EmployeesService : IEmployeesService
    {
        private IUnitOfWork _uow;
        private ICryptoEngine _cryptography;
        public EmployeesService(IUnitOfWork uow, ICryptoEngine cryptography)
        {
            _uow = uow;
            _cryptography = cryptography;

        }

        public IEnumerable<Employee> GetEmployees(int clientId)
        {
            var employees = _uow.Employees.Find(e => e.ClientId == clientId);
            foreach (var employee in employees)
            {
                if (!String.IsNullOrEmpty(employee.Ssn))
                {
                    employee.Ssn = _cryptography.Decrypt(employee.Ssn);
                }
                
            }

            return employees;
            
        }

        public Employee Add(Employee dto, int clientId)
        {
            var target = _uow.Employees.FirstOrDefault(e => e.EmployeeId == dto.EmployeeId && e.ClientId == clientId);
            if (target == null)
            {
                dto.ClientId = clientId;
                dto.Ssn = _cryptography.Encrypt(dto.Ssn);
                _uow.Employees.Add(dto);
            }

            _uow.Commit();
            return dto;
        }

        public Employee Update(Employee dto, int clientId)
        {

            var target = _uow.Employees.FirstOrDefault(e => e.EmployeeId == dto.EmployeeId && e.ClientId == clientId);
            if (target != null)
            {
                target.Prefix = dto.Prefix;
                target.FirstName = dto.FirstName;
                target.LastName = dto.LastName;
                target.MiddleInitial = dto.MiddleInitial;
                target.NameToPrintOnCheck = dto.NameToPrintOnCheck;
                target.Ssn = _cryptography.Encrypt(dto.Ssn);
                target.Address1 = dto.Address1;
                target.Address2 = dto.Address2;
                target.City = dto.City;
                target.State = dto.State;
                target.ZipCode = dto.ZipCode;
                target.Allowances = dto.Allowances;
                target.MaritalStatus = dto.MaritalStatus;
                target.TaxForm = dto.TaxForm;
                target.Notes = dto.Notes;
                //target.PhoneNumber = dto.PhoneNumber;
                //target.DateOfBirth = dto.DateOfBirth;
                target.IsActive = dto.IsActive;
                target.PayRate = dto.PayRate;
            }


            _uow.Commit();
            return target;
        }

        public void Delete(int employeeId, int clientId)
        {
            
            var employee = _uow.Employees.FirstOrDefault(e => e.EmployeeId == employeeId && e.ClientId == clientId);
            _uow.Employees.Delete(employee);
            _uow.Commit();


        }
    }


}
