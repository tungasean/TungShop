namespace TungShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnew1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentDisciplines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        InfoDiscipline = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Students", "IsOut", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "IsOut");
            DropTable("dbo.StudentDisciplines");
        }
    }
}
