namespace MovieReviews_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewsToMovie : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Genres", newName: "Genre");
            RenameTable(name: "dbo.Movies", newName: "Movie");
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Body = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        Title = c.String(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.Movie_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "Movie_Id", "dbo.Movie");
            DropIndex("dbo.Review", new[] { "Movie_Id" });
            DropTable("dbo.Review");
            RenameTable(name: "dbo.Movie", newName: "Movies");
            RenameTable(name: "dbo.Genre", newName: "Genres");
        }
    }
}
