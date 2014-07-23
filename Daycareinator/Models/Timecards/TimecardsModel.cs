using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Timecards
{
    public class TimecardsModel
    {
        public IEnumerable<EmployeeTimecard> EmployeeTimecards { get; set; }
        public IEnumerable<DateModel> Dates { get; set; }
        public bool IsTimecardClosed { get; set; }
    }
}