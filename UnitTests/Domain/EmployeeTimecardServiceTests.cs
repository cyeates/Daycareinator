using Daycareinator.Data;
using Daycareinator.Data.Entities;
using Daycareinator.Domain.Extensions;
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
    public class EmployeeTimecardServiceTests
    {
        private Daycareinator.Domain.Dtos.EmployeeDto _employee;
        private EmployeeTimecardService _service;
        private InMemoryRepository<Employee> _repo;
        private int _clientId = 1;
        private InMemoryRepository<TimecardSubmission> _timecardSubmissionRepo;
        private DateTime _date;
        private Mock<IUnitOfWork> _uow;
        private Mock<ICryptoEngine> _crypto;

        [TestInitialize]
        public void SetUp()
        {
            _repo = new InMemoryRepository<Employee>();
            _timecardSubmissionRepo = new InMemoryRepository<TimecardSubmission>();
            _date = DateTime.Today;
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(u => u.Employees).Returns(_repo);
            _uow.Setup(u => u.TimecardSubmissions).Returns(_timecardSubmissionRepo);
            AddTestEmployees(_repo);

            _crypto = new Mock<ICryptoEngine>();
            _crypto.Setup(c => c.Decrypt(It.IsAny<string>())).Returns<string>(ssn => ssn);
            _service = new EmployeeTimecardService(_uow.Object, _crypto.Object);

        }
        [TestMethod]
        public void GetTimecardEntriesForSpecifiedWeek()
        {
            var timecard = _service.GetTimecard(clientId: 1, weekOf: new DateTime(2013, 6, 20));
            _employee = timecard.Employees.ToList()[0];
            Assert.AreEqual(7, _employee.TimecardEntries.Count());

        }

        [TestMethod]
        public void CalculateTotalsWithOvertime()
        {
            var timecard = _service.GetTimecard(clientId: 1, weekOf: new DateTime(2013, 6, 20));
            _employee = timecard.Employees.ToList()[0];
            Assert.AreEqual(9.00M, _employee.RegularPayRate, "Regular pay rate");
            Assert.AreEqual(13.50M, _employee.OTPayRate, "OT pay rate");
            Assert.AreEqual(40, _employee.RegularHours, "Regular hours");
            Assert.AreEqual(1, _employee.OtHours, "OT hours");
            Assert.AreEqual(41, _employee.TotalHours, "Total hours");
            Assert.AreEqual(373.50M, _employee.GrossPay, "Gross pay");
        }

        [TestMethod]
        public void CalculateTotalsWithoutOvertime()
        {

            var timecard = _service.GetTimecard(clientId: 1, weekOf: new DateTime(2013, 6, 20));
            _employee = timecard.Employees.ToList()[1];

            Assert.AreEqual(9.00M, _employee.RegularPayRate, "Regular pay rate");
            Assert.AreEqual(13.50M, _employee.OTPayRate, "OT pay rate");
            Assert.AreEqual(7, _employee.RegularHours, "Regular hours");
            Assert.AreEqual(0, _employee.OtHours, "OT hours");
            Assert.AreEqual(7, _employee.TotalHours, "Total hours");
            Assert.AreEqual(63.00M, _employee.GrossPay, "Gross pay");
        }

        [TestMethod]
        public void UpdateTimeEntries()
        {
            var employeeEntities = _repo.GetAll().ToList();
            var testDate = employeeEntities[0].TimecardEntries[0].Date;
            decimal hours = 20;


            var employeeDtos = new List<Employee>
                {
                    new Employee
                        {
                            EmployeeId = employeeEntities[0].EmployeeId, 
                            TimecardEntries = new List<TimecardEntry>{new TimecardEntry{Date = testDate, Hours = hours}}
                         },

                };

            _service.Save(employeeDtos, _clientId, _date);

            Assert.AreEqual(employeeDtos[0].TimecardEntries[0].Hours, employeeEntities[0].TimecardEntries[0].Hours);
        }

        [TestMethod]
        public void AddTimeEntriesIfTheyDontExist()
        {
            var testDate = DateTime.Today;
            var employee = new Employee { EmployeeId = 3, ClientId = _clientId };
            _repo.Add(employee);

            decimal hours = 20;


            var employeeDtos = new List<Employee>
                {
                    new Employee
                        {
                            EmployeeId = employee.EmployeeId, 
                            TimecardEntries = new List<TimecardEntry>{new TimecardEntry{Date = testDate, Hours = 12} }
                         },

                };

            _service.Save(employeeDtos, _clientId, _date);

            Assert.AreEqual(employeeDtos[0].TimecardEntries[0].Date, employee.TimecardEntries[0].Date);
            Assert.AreEqual(employeeDtos[0].TimecardEntries[0].Hours, employee.TimecardEntries[0].Hours);
        }

        [TestMethod]
        public void DoNotSaveIfTimecardHasAlreadyBeenSubmitted()
        {
            _timecardSubmissionRepo.Add(new TimecardSubmission { ClientId = _clientId, FirstDateOfTimecard = _date.GetFirstDayOfWeek().Date });
            var result = _service.Save(new List<Employee>(), _clientId, _date);

            Assert.IsFalse(result.IsValid);
            _uow.Verify(u => u.Commit(), Times.Never());
        }

        [TestMethod]
        public void GetLast4Ssn()
        {
            var timecard = _service.GetTimecard(clientId: 1, weekOf: new DateTime(2013, 6, 20));
            var employees = timecard.Employees.ToList();
            Assert.AreEqual("6789", employees[0].Last4Ssn, "Ssn Formated with dashes");
            Assert.AreEqual("6789", employees[1].Last4Ssn, "Ssn formated without dashes");
        }
        private void AddTestEmployees(InMemoryRepository<Employee> repo)
        {
            //employee with OT
            repo.Add(new Employee
                         {
                             EmployeeId = 1,
                             ClientId = 1,
                             PayRate = 9.00M,
                             Ssn = "123-45-6789",
                             TimecardEntries = new List<TimecardEntry>{
                                new TimecardEntry{Date = new DateTime(2013, 6, 16)},
                                new TimecardEntry{Date = new DateTime(2013, 6, 17), Hours = 8},
                                new TimecardEntry{Date = new DateTime(2013, 6, 18), Hours = 9},
                                new TimecardEntry{Date = new DateTime(2013, 6, 19), Hours = 8},
                                new TimecardEntry{Date = new DateTime(2013, 6, 20), Hours = 8},
                                new TimecardEntry{Date = new DateTime(2013, 6, 21), Hours = 8},
                                new TimecardEntry{Date = new DateTime(2013, 6, 22), Hours = 0},
                                new TimecardEntry{Date = new DateTime(2013, 6, 23), Hours = 0},
                                new TimecardEntry{Date = new DateTime(2013, 6, 24), Hours = 8}

                         }
                         });

            //no OT
            repo.Add(new Employee
                         {
                             EmployeeId = 2,
                             ClientId = 1,
                             PayRate = 9.00M,
                             Ssn = "123456789",
                             TimecardEntries = new List<TimecardEntry>{
                                new TimecardEntry{Date = new DateTime(2013, 6, 16)},
                                new TimecardEntry{Date = new DateTime(2013, 6, 17), Hours = 1},
                                new TimecardEntry{Date = new DateTime(2013, 6, 18), Hours = 1},
                                new TimecardEntry{Date = new DateTime(2013, 6, 19), Hours = 1},
                                new TimecardEntry{Date = new DateTime(2013, 6, 20), Hours = 1},
                                new TimecardEntry{Date = new DateTime(2013, 6, 21), Hours = 1},
                                new TimecardEntry{Date = new DateTime(2013, 6, 22), Hours = 1},
                                new TimecardEntry{Date = new DateTime(2013, 6, 23), Hours = 1},
                                new TimecardEntry{Date = new DateTime(2013, 6, 24), Hours = 1}

                         }
                         });
        }

    }

}
