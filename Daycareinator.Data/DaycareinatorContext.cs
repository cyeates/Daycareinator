using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Data
{
  public class DaycareinatorContext : DbContext
  {
      public DbSet<Child> Children { get; set; }
      public DbSet<Client> Clients { get; set; }
      public DbSet<Employee> Employees { get; set; }
      public DbSet<Record> Records { get; set; }
      public DbSet<TimecardSubmission> TimecardSubmissions { get; set; }
      public DbSet<User> Users { get; set; }

      
      
  }
}
