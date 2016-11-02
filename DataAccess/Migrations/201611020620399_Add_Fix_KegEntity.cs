namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Fix_KegEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kegs", "TapId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kegs", "TapId");
        }
    }
}
