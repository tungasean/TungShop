namespace TungShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ElectricityWaterHistorys");
            AddColumn("dbo.ElectricityWaterHistorys", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ElectricityWaterHistorys", "RoomID", c => c.String(nullable: false));
            AddPrimaryKey("dbo.ElectricityWaterHistorys", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ElectricityWaterHistorys");
            AlterColumn("dbo.ElectricityWaterHistorys", "RoomID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.ElectricityWaterHistorys", "ID");
            AddPrimaryKey("dbo.ElectricityWaterHistorys", "RoomID");
        }
    }
}
