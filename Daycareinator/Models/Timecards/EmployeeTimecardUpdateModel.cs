using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Timecards
{
    public class EmployeeTimecardUpdateModel
    {
        public int EmployeeId { get; set; }
        public IEnumerable<TimecardEntry> TimecardEntries { get; set; }
        
        public Employee ToEntity()
        {
            return new Employee
            {
                EmployeeId = this.EmployeeId,
                TimecardEntries = GetTimecardEntries()
            };
        }

        private IList<Data.Entities.TimecardEntry> GetTimecardEntries()
        {
            var entries = new List<Data.Entities.TimecardEntry>();
            foreach (var entry in this.TimecardEntries)
            {
                
                    entries.Add(new Data.Entities.TimecardEntry
                    {
                        Date = entry.Date,
                        Hours = entry.Hours.HasValue ? entry.Hours.Value : 0
                    });
                
                
            }

            return entries;
        }
    }

    
}