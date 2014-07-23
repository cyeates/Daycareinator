namespace Daycareinator.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Children",
                c => new
                    {
                        ChildId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ChildId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Prefix = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleInitial = c.String(),
                        NameToPrintOnCheck = c.String(),
                        Ssn = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        PhoneNumber = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        PayRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.TimecardEntries",
                c => new
                    {
                        TimecardEntryId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TimecardEntryId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        RecordId = c.Int(nullable: false),
                        RecordType = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateTable(
                "dbo.TimecardSubmissions",
                c => new
                    {
                        TimecardSubmissionId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        FirstDateOfTimecard = c.DateTime(nullable: false),
                        DateSubmitted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TimecardSubmissionId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.webpages_Membership",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CreateDate = c.DateTime(),
                        ConfirmationToken = c.String(maxLength: 128),
                        IsConfirmed = c.Boolean(),
                        LastPasswordFailureDate = c.DateTime(),
                        PasswordFailuresSinceLastSuccess = c.Int(nullable: false),
                        Password = c.String(nullable: false, maxLength: 128),
                        PasswordChangedDate = c.DateTime(),
                        PasswordSalt = c.String(nullable: false, maxLength: 128),
                        PasswordVerificationToken = c.String(maxLength: 128),
                        PasswordVerificationTokenExpirationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.webpages_Membership", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ClientId" });
            DropIndex("dbo.TimecardEntries", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "ClientId" });
            DropIndex("dbo.Children", new[] { "ClientId" });
            DropForeignKey("dbo.webpages_Membership", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.TimecardEntries", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Children", "ClientId", "dbo.Clients");
            DropTable("dbo.webpages_Membership");
            DropTable("dbo.Users");
            DropTable("dbo.TimecardSubmissions");
            DropTable("dbo.Records");
            DropTable("dbo.TimecardEntries");
            DropTable("dbo.Employees");
            DropTable("dbo.Clients");
            DropTable("dbo.Children");
        }
    }
}
