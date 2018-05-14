namespace TungShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Approvals", "Sex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Approvals", "Sex", c => c.String(maxLength: 15));
        }
    }
}
