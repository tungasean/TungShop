namespace TungShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update31 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Approvals",
                c => new
                    {
                        ApprovalId = c.Int(nullable: false, identity: true),
                        StudentId = c.String(),
                        Name = c.String(),
                        BirthDay = c.DateTime(),
                        Sex = c.String(maxLength: 15),
                        CardNo = c.String(maxLength: 20),
                        Address = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ApprovalId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Approvals");
        }
    }
}
