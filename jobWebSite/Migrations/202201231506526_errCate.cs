namespace jobWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class errCate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RollViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RollViewModels");
        }
    }
}
