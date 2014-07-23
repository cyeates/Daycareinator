using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Dtos
{
    public class TimecardDto
    {
        public IEnumerable<EmployeeDto> Employees { get; set; }
        public bool IsTimecardClosed { get; set; }
    }

    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Last4Ssn { get; set; }
        public decimal RegularPayRate { get; set; }

        public decimal OTPayRate 
        { 
            get 
            { 
                return Math.Round(this.RegularPayRate * 1.5M, 2); 
            } 
        }

        public decimal RegularHours 
        {
            get 
            {
                decimal totalHours = TotalHours;
                var regular = totalHours >= 40 ? 40 : totalHours;
                return Math.Round(regular, 2);
            }

        }

        public decimal OtHours
        { 
            get 
            {
                decimal totalHours = TotalHours;
                var ot = totalHours <= 40 ? 0 : totalHours - 40;
                return Math.Round(ot, 2);
            } 
        }

        public decimal TotalHours 
        { 
            get 
            {
                decimal totalHours = (decimal)TimecardEntries.Sum(t => t.Hours);
                return Math.Round(totalHours, 2);
            } 
        }

        public decimal GrossPay 
        {
            get 
            {
                return Math.Round((RegularPayRate * RegularHours) + (OTPayRate * OtHours), 2);
            }
            
        }
        
        public IEnumerable<TimecardEntryDto> TimecardEntries {get; set;}

        
    }

    public class TimecardEntryDto
    {
        public DateTime Date {get; set;}
        public decimal? Hours { get; set; }
    }
}
