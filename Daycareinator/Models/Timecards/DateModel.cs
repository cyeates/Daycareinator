using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Timecards
{
    public class DateModel
    {
        public DateTime Date { get; set; }
        public string DayOfWeek
        {
            get
            {
                return Date.DayOfWeek.ToString();
            }
        }

        public string DisplayString
        {
            get
            {
                return Date.ToShortDateString();
            }
        }

        public DateModel(DateTime date)
        {
            this.Date = date;
        }

        public DateModel() { }

        
    }
}