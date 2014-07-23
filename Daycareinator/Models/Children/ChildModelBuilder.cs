using Daycareinator.Data.Entities;
using Daycareinator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Children
{
    public interface IChildModelBuilder
    {
        ChildModel Build(Child child);
    }
    public class ChildModelBuilder : IChildModelBuilder
    {
       
        private IEnumerable<Record> _records;
        public ChildModelBuilder(IRecordsService recordsService)
        {
            _records = recordsService.GetRecordsByType(RecordType.Child);
        }

        public ChildModel Build(Child child)
        {
            return new ChildModel 
                { 
                    Name = string.Format("{0} {1}", child.FirstName, child.LastName),
                    Records = GetRecords(child)
                };
        }

        private IEnumerable<ChildRecordModel> GetRecords(Child child)
        {
            var childRecords = new List<ChildRecordModel>();
            foreach (var record in _records)
            {
                var childRecord = child.Records.FirstOrDefault(c => c.RecordId == record.RecordId);
                if (childRecord != null)
                {
                    childRecords.Add(new ChildRecordModel(childRecord, record));
                }
                else
                {
                    childRecords.Add(new ChildRecordModel(record));
                }
            }

            return childRecords;
        }
    }
}