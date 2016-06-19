namespace Memoirs.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAppSettingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Key = c.String(maxLength: 120),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.AppSettings", new[] { "Key" });
            DropTable("dbo.AppSettings");
        }
    }
}
