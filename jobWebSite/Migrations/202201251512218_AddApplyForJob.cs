namespace jobWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplyForJob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyForJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        JobId = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.JobId)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForJobs", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplyForJobs", "JobId", "dbo.Jobs");
            DropIndex("dbo.ApplyForJobs", new[] { "UserID" });
            DropIndex("dbo.ApplyForJobs", new[] { "JobId" });
            DropTable("dbo.ApplyForJobs");
        }
    }
}
