namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BeerTapV2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Taps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 50),
                        ContentInMl = c.Int(nullable: false),
                        OfficeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Offices", t => t.OfficeId, cascadeDelete: true)
                .Index(t => t.OfficeId);
            CreateTable(
                "dbo.Kegs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Content = c.Int(nullable: false),
                    MaxContent = c.Int(nullable: false),
                    UnitOfMeasurement = c.String(),
                    TapId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Taps", "OfficeId", "dbo.Offices");
            DropIndex("dbo.Taps", new[] { "OfficeId" });
            DropTable("dbo.Taps");
            DropTable("dbo.Offices");
        }
    }
}
