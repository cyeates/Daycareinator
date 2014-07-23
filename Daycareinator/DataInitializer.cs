using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Daycareinator.Data;
using Daycareinator.Data.Entities;
using WebMatrix.WebData;
using Daycareinator.Models;
using Daycareinator.Domain.Extensions;
using System.Web.Security;

namespace Daycareinator
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DaycareinatorContext>
    {
        protected override void Seed(DaycareinatorContext context)
        {
           
            var client = new Client { Name = "chad" };
            context.Clients.Add(client);
            context.SaveChanges();
                     

            var records = GetRecords();
            records.ForEach(r => context.Records.Add(r));

            var children = GetChildren(client.ClientId);
            children.ForEach(c => context.Children.Add(c));

            var employees = GetEmployeesWithTimecards(client.ClientId);
            employees.ForEach(e => context.Employees.Add(e));

            context.SaveChanges();

            WebSecurity.InitializeDatabaseConnection("DaycareinatorContext", "Users", "UserId", "EmailAddress", true);

            var membership = (SimpleMembershipProvider)System.Web.Security.Membership.Provider;
            membership.CreateUserAndAccount("chad.yeates@gmail.com", "password", new Dictionary<string, object> { { "ClientId", client.ClientId } });

            if (!Roles.RoleExists("Admin"))
            {
                Roles.CreateRole("Admin");
            }

            Roles.AddUserToRole("chad.yeates@gmail.com", "Admin");

            



           
            
           
            

            //var timecards = GetTimeCards();
        }

        private List<Employee> GetEmployeesWithTimecards(int clientId)
        {
            DateTime firstDayOfWeek = DateTime.Now.GetFirstDayOfWeek();
            return new List<Employee>
            {
                 new Employee{FirstName = "Homer", LastName = "Simpson", ClientId = clientId, PayRate = 9.00M, DateOfBirth = DateTime.Today, TimecardEntries = GetRandomTimecardEntriees(firstDayOfWeek)},
                new Employee{FirstName = "Ned", LastName = "Flanders", ClientId = clientId, PayRate = 15.00M, DateOfBirth = DateTime.Today, TimecardEntries = GetRandomTimecardEntriees(firstDayOfWeek)},
                new Employee{FirstName = "Moe", LastName = "Syzlak", ClientId = clientId, PayRate = 10.50M, DateOfBirth = DateTime.Today, TimecardEntries = GetRandomTimecardEntriees(firstDayOfWeek)}
                //new Employee{FirstName = "Homer", LastName = "Simpson", ClientId = clientId, PayRate = 9.00M}, //TimecardEntries = GetRandomTimecardEntriees(firstDayOfWeek)},
                //new Employee{FirstName = "Ned", LastName = "Flanders", ClientId = clientId, PayRate = 15.00M}, //TimecardEntries = GetRandomTimecardEntriees(firstDayOfWeek)},
                //new Employee{FirstName = "Moe", LastName = "Syzlak", ClientId = clientId, PayRate = 10.50M}, //TimecardEntries = GetRandomTimecardEntriees(firstDayOfWeek)}
            };
        }

        private IList<TimecardEntry> GetRandomTimecardEntriees(DateTime firstDayOfWeek)
        {
            var entries = new List<TimecardEntry>();
           
            //entries for current week
            for (int i = 0; i <= 6; i++)
            {
                var random = new Random(i);
                entries.Add(new TimecardEntry
                {
                     Date = firstDayOfWeek.AddDays(i).Date,
                     Hours  = random.Next(0, 10)
                });
            }

            //a couple of entries for previous week
            for (int i = -3; i <= -2; i++)
            {
                var random = new Random(i);
                entries.Add(new TimecardEntry
                {
                    Date = firstDayOfWeek.AddDays(i).Date,
                    Hours = random.Next(0, 10)
                });
            }

            return entries;
        }

        private List<Child> GetChildren(int clientId)
        {
            return new List<Child>
            {
                new Child {ClientId = clientId, FirstName = "Bart", LastName = "Simpson", IsActive = true}
            };
        }

        private List<Record> GetRecords()
        {
            return new List<Record>
            {
                new Record{RecordId = 1, RecordType = RecordType.Child, Description = "Admission Information"},
                new Record{RecordId = 7, RecordType = RecordType.Child, Description = "Health Care Orders"},
                new Record{RecordId = 2, RecordType = RecordType.Child, Description = "Health Care Statement"},
                new Record{RecordId = 3, RecordType = RecordType.Child, Description = "Immunization Records"},
                new Record{RecordId = 4, RecordType = RecordType.Child, Description = "Hearing and Vision Test"},
                new Record{RecordId = 5, RecordType = RecordType.Child, Description = "Incident/Illness Report Form"},
                new Record{RecordId = 6, RecordType = RecordType.Child, Description = "Medication Records"},
                new Record{RecordId = 8, RecordType = RecordType.Child, Description = "Infant Feeding Instructions"}
            };
        }

        //private object GetTimeCards()
        //{
        //  return new List<Timecard>
        //    {
        //      new Timecard
        //        {

        //        };
        //    };
        //}
    }
}