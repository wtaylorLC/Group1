namespace MovieReviews_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentType = c.Byte(nullable: false),
                        AuthorId = c.String(),
                        CommentBody = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CommentParentId = c.Int(),
                        CommentSubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comment");
        }
    }
}
