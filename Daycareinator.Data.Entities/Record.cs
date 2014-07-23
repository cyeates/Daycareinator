using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Data.Entities
{
    public class Record
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecordId { get; set; }
        public RecordType RecordType { get; set; }
        public string Description { get; set; }
    }

    public enum RecordType 
    {
        Child = 1,
        Employee = 2
    }
}
