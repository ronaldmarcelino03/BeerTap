namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEntitiesAndModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kegs", "OfficeId", c => c.Int(nullable: false));
            AddColumn("dbo.Taps", "KegState", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Taps", "KegState");
            DropColumn("dbo.Kegs", "OfficeId");
        }
    }
}
