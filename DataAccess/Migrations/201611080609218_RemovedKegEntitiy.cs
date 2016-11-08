namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedKegEntitiy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Taps", "BeerName", c => c.String());
            AddColumn("dbo.Taps", "Content", c => c.Int(nullable: false));
            AddColumn("dbo.Taps", "MaxContent", c => c.Int(nullable: false));
            AddColumn("dbo.Taps", "UnitOfMeasurement", c => c.String());
            DropColumn("dbo.Taps", "Name");
            DropColumn("dbo.Taps", "KegId");
            DropTable("dbo.Kegs");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Taps", "KegId", c => c.Int(nullable: false));
            AddColumn("dbo.Taps", "Name", c => c.String());
            DropColumn("dbo.Taps", "UnitOfMeasurement");
            DropColumn("dbo.Taps", "MaxContent");
            DropColumn("dbo.Taps", "Content");
            DropColumn("dbo.Taps", "BeerName");
        }
    }
}
