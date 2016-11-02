namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_FixTapEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Taps", "Name", c => c.String());
            DropColumn("dbo.Taps", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Taps", "Location", c => c.String());
            DropColumn("dbo.Taps", "Name");
        }
    }
}
