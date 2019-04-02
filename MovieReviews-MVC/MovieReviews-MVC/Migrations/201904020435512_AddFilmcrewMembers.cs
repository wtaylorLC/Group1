namespace MovieReviews_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilmcrewMembers : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FilmCrew", newName: "FilmCrewMember");
            RenameTable(name: "dbo.FilmCrewMovie", newName: "MovieFilmCrewMember");
            RenameColumn(table: "dbo.MovieFilmCrewMember", name: "FilmCrew_Id", newName: "FilmCrewMember_Id");
            RenameIndex(table: "dbo.MovieFilmCrewMember", name: "IX_FilmCrew_Id", newName: "IX_FilmCrewMember_Id");
            DropPrimaryKey("dbo.MovieFilmCrewMember");
            AddPrimaryKey("dbo.MovieFilmCrewMember", new[] { "Movie_Id", "FilmCrewMember_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MovieFilmCrewMember");
            AddPrimaryKey("dbo.MovieFilmCrewMember", new[] { "FilmCrew_Id", "Movie_Id" });
            RenameIndex(table: "dbo.MovieFilmCrewMember", name: "IX_FilmCrewMember_Id", newName: "IX_FilmCrew_Id");
            RenameColumn(table: "dbo.MovieFilmCrewMember", name: "FilmCrewMember_Id", newName: "FilmCrew_Id");
            RenameTable(name: "dbo.MovieFilmCrewMember", newName: "FilmCrewMovie");
            RenameTable(name: "dbo.FilmCrewMember", newName: "FilmCrew");
        }
    }
}
