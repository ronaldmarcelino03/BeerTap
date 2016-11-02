namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Fix_Entities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kegs", "UnitOfMeasurement", c => c.String());
            AddColumn("dbo.Taps", "Location", c => c.String());
            DropColumn("dbo.Kegs", "Unit");
            DropColumn("dbo.Kegs", "TapId");
            DropColumn("dbo.Taps", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Taps", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Kegs", "TapId", c => c.Int(nullable: false));
            AddColumn("dbo.Kegs", "Unit", c => c.String());
            DropColumn("dbo.Taps", "Location");
            DropColumn("dbo.Kegs", "UnitOfMeasurement");
        }
    }
}
