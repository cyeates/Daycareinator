using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Timecards
{
    public class TimecardUpdateModel
    {
        public IEnumerable<EmployeeTimecardUpdateModel> EmployeeTimecards { get; set; }
        public DateModel Date { get; set; }
    }
}