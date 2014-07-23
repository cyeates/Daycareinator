using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Timecards
{
    public class EmployeeTimecard
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Last4Ssn { get; set; }
        public decimal RegularPayRate { get; set; }
        public decimal OtPayRate { get; set; }
        public IEnumerable<TimecardEntry> TimecardEntries { get; set; }
        public decimal RegularHours { get; set; }
        public decimal OtHours { get; set; }
        public decimal TotalHours { get; set; }
        public decimal GrossPay { get; set; }
    }
}