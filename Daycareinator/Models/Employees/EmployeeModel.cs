using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Employees
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string Prefix { get; set; }

        public string FirstName { get; set; }

        
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string NameToPrintOnCheck { get; set; }

        
        [Required]
        [RegularExpression("^\\d{3}-\\d{2}-\\d{4}$")]
        public string Ssn { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
       // public string PhoneNumber { get; set; }
        //public DateTime DateOfBirth { get; set; }
        public string Notes { get; set; }
        public int Allowances { get; set; }
        public string MaritalStatus { get; set; }
        public string TaxForm { get; set; }
        
        public decimal PayRate { get; set; }
        public bool IsActive { get; set; }

       
    }
}