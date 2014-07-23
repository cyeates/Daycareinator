using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Data.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string NameToPrintOnCheck { get; set; }
        public string Ssn { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        //public string PhoneNumber { get; set; }
        //public DateTime DateOfBirth { get; set; }
        public decimal PayRate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public int Allowances { get; set; }
        public string MaritalStatus { get; set; }
        public string TaxForm { get; set; }

        public virtual IList<TimecardEntry> TimecardEntries { get; set; }
        public virtual Client Client { get; set; }

        public Employee()
        {
           TimecardEntries = new List<TimecardEntry>();
        }

    }
}
