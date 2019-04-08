namespace MovieReviews_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReportedComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportedComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Reason = c.String(),
                        Resolved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comment", t => t.CommentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportedComment", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReportedComment", "CommentId", "dbo.Comment");
            DropIndex("dbo.ReportedComment", new[] { "UserId" });
            DropIndex("dbo.ReportedComment", new[] { "CommentId" });
            DropTable("dbo.ReportedComment");
        }
    }
}
