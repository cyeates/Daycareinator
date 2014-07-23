using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Children
{
    public class ChildGridModel
    {
        public int ChildId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
               
        public bool IsActive { get; set; }

        /// <summary>
        /// .error if the child is missing records
        /// </summary>
        public string RowClass { get; set; }

        
    }
}