namespace TungShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ElectricityWaterHistorys",
                c => new
                    {
                        RoomID = c.String(nullable: false, maxLength: 128),
                        Month = c.String(),
                        WaterNew = c.Int(nullable: false),
                        WaterOld = c.Int(nullable: false),
                        EletricityOld = c.Int(nullable: false),
                        EletricityNew = c.Int(nullable: false),
                        Money = c.Int(nullable: false),
                        PriceElectricity = c.Int(nullable: false),
                        PriceWater = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        TimeChange = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RoomID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ElectricityWaterHistorys");
        }
    }
}
