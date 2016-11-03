namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRecords2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Kegs", "KegState");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kegs", "KegState", c => c.String());
        }
    }
}
