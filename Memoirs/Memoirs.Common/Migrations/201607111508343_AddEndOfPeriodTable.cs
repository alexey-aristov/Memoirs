namespace Memoirs.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEndOfPeriodTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EndOfPeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Description = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(),
                        Week = c.Int(),
                        EndOfPeriodType = c.Int(nullable: false),
                        RecordId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Records", t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.Id)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Records", "EndOfPeriodId", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EndOfPeriods", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EndOfPeriods", "Id", "dbo.Records");
            DropIndex("dbo.EndOfPeriods", new[] { "UserId" });
            DropIndex("dbo.EndOfPeriods", new[] { "Id" });
            DropColumn("dbo.Records", "EndOfPeriodId");
            DropTable("dbo.EndOfPeriods");
        }
    }
}
