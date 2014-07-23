namespace Daycareinator.Data.Migrations
{
    using Daycareinator.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<Daycareinator.Data.DaycareinatorContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Daycareinator.Data.DaycareinatorContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var client = new Client {ClientId = 1, Name = "chad" };
            context.Clients.AddOrUpdate(client);
            context.SaveChanges();


            var records = GetRecords();
            records.ForEach(r => context.Records.AddOrUpdate(r));
            context.SaveChanges();

            WebSecurity.InitializeDatabaseConnection("DaycareinatorContext", "Users", "UserId", "EmailAddress", true);

            
            var membership = (SimpleMembershipProvider)System.Web.Security.Membership.Provider;
            var adminUser = membership.GetUser("chad.yeates@gmail.com", false);
            if (adminUser == null)
                membership.CreateUserAndAccount("chad.yeates@gmail.com", "password", new Dictionary<string, object> { { "ClientId", client.ClientId } });
                             

            if (!Roles.RoleExists("Admin"))
            {
                Roles.CreateRole("Admin");
            }

            if (!Roles.IsUserInRole("chad.yeates@gmail.com", "Admin"))
            {
                Roles.AddUserToRole("chad.yeates@gmail.com", "Admin");
                
            }

            
        }
               

        private List<Record> GetRecords()
        {
            return new List<Record>
            {
                new Record{RecordId = 1, RecordType = RecordType.Child, Description = "Admission Information"},
                new Record{RecordId = 7, RecordType = RecordType.Child, Description = "Health Care Orders"},
                new Record{RecordId = 2, RecordType = RecordType.Child, Description = "Health Care Statement"},
                new Record{RecordId = 3, RecordType = RecordType.Child, Description = "Immunization Records"},
                new Record{RecordId = 4, RecordType = RecordType.Child, Description = "Hearing and Vision Test"},
                new Record{RecordId = 5, RecordType = RecordType.Child, Description = "Incident/Illness Report Form"},
                new Record{RecordId = 6, RecordType = RecordType.Child, Description = "Medication Records"},
                new Record{RecordId = 8, RecordType = RecordType.Child, Description = "Infant Feeding Instructions"}
            };
        }
    }
}
