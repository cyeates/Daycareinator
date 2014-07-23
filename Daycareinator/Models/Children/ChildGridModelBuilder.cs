using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Children
{
    public class ChildGridModelBuilder
    {
        private Child _child;
        private IEnumerable<Record> _records;
        public ChildGridModelBuilder(IEnumerable<Record> records)
        {
            
            _records = records;
        }
        public ChildGridModel Build(Child child)
        {

            return new ChildGridModel 
                            { 
                                ChildId = child.ChildId, 
                                FirstName = child.FirstName, 
                                LastName = child.LastName,
                                IsActive = child.IsActive,
                                RowClass = child.Records == null || child.Records.Count(c => !String.IsNullOrEmpty(c.DisplayFileName)) != _records.Count() ? "error" : string.Empty
                            };
        }
    }
}