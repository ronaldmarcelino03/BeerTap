namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxContent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Taps", "OfficeId", "dbo.Offices");
            DropIndex("dbo.Taps", new[] { "OfficeId" });
            AddColumn("dbo.Taps", "Content", c => c.Int(nullable: false));
            AddColumn("dbo.Taps", "MaxContent", c => c.Int(nullable: false));
            AddColumn("dbo.Taps", "KegState", c => c.String());
            DropColumn("dbo.Taps", "Description");
            DropColumn("dbo.Taps", "ContentInMl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Taps", "ContentInMl", c => c.Int(nullable: false));
            AddColumn("dbo.Taps", "Description", c => c.String(maxLength: 50));
            DropColumn("dbo.Taps", "KegState");
            DropColumn("dbo.Taps", "MaxContent");
            DropColumn("dbo.Taps", "Content");
            CreateIndex("dbo.Taps", "OfficeId");
            AddForeignKey("dbo.Taps", "OfficeId", "dbo.Offices", "Id", cascadeDelete: true);
        }
    }
}
