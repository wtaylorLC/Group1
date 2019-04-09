namespace MovieReviews_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostIdComment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comment", "Post_Id", "dbo.Post");
            DropIndex("dbo.Comment", new[] { "Post_Id" });
            RenameColumn(table: "dbo.Comment", name: "Post_Id", newName: "PostId");
            AlterColumn("dbo.Comment", "PostId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comment", "PostId");
            AddForeignKey("dbo.Comment", "PostId", "dbo.Post", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "PostId", "dbo.Post");
            DropIndex("dbo.Comment", new[] { "PostId" });
            AlterColumn("dbo.Comment", "PostId", c => c.Int());
            RenameColumn(table: "dbo.Comment", name: "PostId", newName: "Post_Id");
            CreateIndex("dbo.Comment", "Post_Id");
            AddForeignKey("dbo.Comment", "Post_Id", "dbo.Post", "Id");
        }
    }
}
