namespace TungShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.RoomAssets");
            AlterColumn("dbo.RoomAssets", "RoomID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.RoomAssets", new[] { "RoomID", "AssetsID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RoomAssets");
            AlterColumn("dbo.RoomAssets", "RoomID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.RoomAssets", new[] { "RoomID", "AssetsID" });
        }
    }
}
