namespace MovieReviews_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManyToManyFilmCrewAndMovies : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movie", "FilmCrewMember_Id", "dbo.FilmCrewMember");
            DropIndex("dbo.Movie", new[] { "FilmCrewMember_Id" });
            CreateTable(
                "dbo.MovieFilmCrewMember",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        FilmCrewMember_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.FilmCrewMember_Id })
                .ForeignKey("dbo.Movie", t => t.Movie_Id)
                .ForeignKey("dbo.FilmCrewMember", t => t.FilmCrewMember_Id)
                .Index(t => t.Movie_Id)
                .Index(t => t.FilmCrewMember_Id);
            
            DropColumn("dbo.Movie", "FilmCrewMember_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movie", "FilmCrewMember_Id", c => c.Int());
            DropForeignKey("dbo.MovieFilmCrewMember", "FilmCrewMember_Id", "dbo.FilmCrewMember");
            DropForeignKey("dbo.MovieFilmCrewMember", "Movie_Id", "dbo.Movie");
            DropIndex("dbo.MovieFilmCrewMember", new[] { "FilmCrewMember_Id" });
            DropIndex("dbo.MovieFilmCrewMember", new[] { "Movie_Id" });
            DropTable("dbo.MovieFilmCrewMember");
            CreateIndex("dbo.Movie", "FilmCrewMember_Id");
            AddForeignKey("dbo.Movie", "FilmCrewMember_Id", "dbo.FilmCrewMember", "Id");
        }
    }
}
