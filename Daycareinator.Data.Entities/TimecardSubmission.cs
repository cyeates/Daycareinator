using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Data.Entities
{
    public class TimecardSubmission
    {
        public int TimecardSubmissionId {get; set;}
        public int ClientId { get; set; }
        public DateTime FirstDateOfTimecard { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
