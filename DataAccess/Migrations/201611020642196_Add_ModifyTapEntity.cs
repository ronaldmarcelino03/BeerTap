namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ModifyTapEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Taps", "KegId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Taps", "KegId");
        }
    }
}
