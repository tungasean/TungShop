namespace TungShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update6 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Contracts");
            AlterColumn("dbo.Contracts", "RoomID", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Contracts", "StudentID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Contracts");
            AlterColumn("dbo.Contracts", "RoomID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Contracts", new[] { "StudentID", "RoomID" });
        }
    }
}
