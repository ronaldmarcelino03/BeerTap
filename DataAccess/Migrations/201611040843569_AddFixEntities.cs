namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFixEntities : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Kegs", "OfficeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kegs", "OfficeId", c => c.Int(nullable: false));
        }
    }
}
