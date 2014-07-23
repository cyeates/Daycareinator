using Daycareinator.Data;
using Daycareinator.Data.Entities;
using Daycareinator.Domain.Services;
using EfficientlyLazy.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Domain
{
    [TestClass]
    public class EmployeeServiceTests
    {
        private InMemoryRepository<Employee> _repo;
        private Mock<IUnitOfWork> _uow;
       private List<Employee> _employees;
       private EmployeesService _service;
       private Mock<ICryptoEngine> _crypto;
        [TestInitialize]
        public void Initialize()
        {
            _crypto = new Mock<ICryptoEngine>();
            _repo = new InMemoryRepository<Employee>();
            _employees = new List<Employee>
            {
                new Employee{EmployeeId = 1, Ssn = "encryptedssn", ClientId = 1},
                
            };

            _repo.AddRange(_employees);

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(u => u.Employees).Returns(_repo);

            _service = new EmployeesService(_uow.Object, _crypto.Object);

        }

        [TestMethod]
        public void SsnIsDecryptedWhenReading()
        {
            var employees = _service.GetEmployees(1);

            _crypto.Verify(c => c.Decrypt(It.IsAny<string>()), Times.Exactly(_employees.Count()));   
        }

        [TestMethod]
        public void DontAttemptToDecryptWhenReadingWhenSsnIsEmpty()
        {
            _employees.ForEach(e => e.Ssn = string.Empty);

            var employees = _service.GetEmployees(1);

            _crypto.Verify(c => c.Decrypt(It.IsAny<string>()), Times.Never());   

        }

        [TestMethod]
        public void SsnIsEncryptedWhenUpdating()
        {
            var dto = new Employee {EmployeeId = 1, ClientId = 1, Ssn = "123456789" };
            _service.Update(dto, clientId: 1);

            _crypto.Verify(c => c.Encrypt(dto.Ssn));

        }

        [TestMethod]
        public void SsnIsEncryptedWhenAdding()
        {
            var dto = new Employee { ClientId = 1, Ssn = "123456789" };
            _service.Add(dto, clientId: 1);

            _crypto.Verify(c => c.Encrypt(It.IsAny<string>()));

        }
    }
}
