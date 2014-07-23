using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Timecards
{
    public class TimecardEntry
    {
        public DateTime Date { get; set; }
        public decimal? Hours { get; set; }
    }
}