using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Children
{
    public class ChildModel
    {
        public string Name { get; set; }
        public IEnumerable<ChildRecordModel> Records { get; set; }
    }

    public class ChildRecordModel
    {
        public string Description { get; set; }
        public string FileName { get; set; }
        public bool NotApplicable { get; set; }
        public string Notes { get; set; }
        public string BeginingDate { get; set; }
        public string NextUpdateDate { get; set; }

        public ChildRecordModel(Record record)
        {
            this.Description = record.Description;
            this.NotApplicable = false;

        }
        public ChildRecordModel(ChildRecord childRecord, Record record)
        {
            this.Description = record.Description;
            this.FileName = childRecord.DisplayFileName;
            this.NotApplicable = childRecord.NotApplicable;
            this.Notes = childRecord.Notes;
            this.BeginingDate = childRecord.BeginingDate.ToShortDateString();
            this.NextUpdateDate = childRecord.NextUpdateDate.ToShortDateString();
        }
    }
}