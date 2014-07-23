using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Data.Entities
{
    public class TimecardEntry
    {
        public int TimecardEntryId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public decimal Hours { get; set; }

      // public virtual Employee Employee { get; set; }
    }
}
