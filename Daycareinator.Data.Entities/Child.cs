using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Data.Entities
{
    public class Child
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ChildId { get; set; }
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName {get; set;}
        public bool IsActive { get; set; }

        public virtual IEnumerable<ChildRecord> Records { get; set; }
        public virtual Client Client { get; set; }

        public Child()
        {
            this.Records = new List<ChildRecord>();

        }
    }
}
