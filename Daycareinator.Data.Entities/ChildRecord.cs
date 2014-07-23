using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Data.Entities
{
    public class ChildRecord
    {
        public int ChildId { get; set; }
        public int RecordId { get; set; }
        public string DisplayFileName { get; set; }
        public string StoredFileName { get; set; }
        public bool NotApplicable { get; set; }
        public string Notes { get; set; }
        public DateTime BeginingDate { get; set; }
        public DateTime NextUpdateDate { get; set; }


        public virtual Child Child { get; set; }
        public virtual Record Record { get; set; }
    }
}
