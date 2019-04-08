namespace MovieReviews_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReviewRatingToFloat : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Review", new[] { "Movie_Id" });
            RenameColumn(table: "dbo.Review", name: "Movie_Id", newName: "MovieId");
            AlterColumn("dbo.Movie", "Rating", c => c.Single(nullable: false));
            AlterColumn("dbo.Review", "MovieId", c => c.Int(nullable: false));
            CreateIndex("dbo.Review", "MovieId");
            DropColumn("dbo.Review", "ReviewedMovieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Review", "ReviewedMovieId", c => c.Int(nullable: false));
            DropIndex("dbo.Review", new[] { "MovieId" });
            AlterColumn("dbo.Review", "MovieId", c => c.Int());
            AlterColumn("dbo.Movie", "Rating", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Review", name: "MovieId", newName: "Movie_Id");
            CreateIndex("dbo.Review", "Movie_Id");
        }
    }
}
