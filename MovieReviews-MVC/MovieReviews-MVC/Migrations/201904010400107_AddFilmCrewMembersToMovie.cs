namespace MovieReviews_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilmCrewMembersToMovie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilmCrew",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Bio = c.String(),
                        DoB = c.DateTime(nullable: false),
                        ImageUri = c.String(),
                        Role = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FilmCrewMovie",
                c => new
                    {
                        FilmCrew_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilmCrew_Id, t.Movie_Id })
                .ForeignKey("dbo.FilmCrew", t => t.FilmCrew_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movie", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.FilmCrew_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilmCrewMovie", "Movie_Id", "dbo.Movie");
            DropForeignKey("dbo.FilmCrewMovie", "FilmCrew_Id", "dbo.FilmCrew");
            DropIndex("dbo.FilmCrewMovie", new[] { "Movie_Id" });
            DropIndex("dbo.FilmCrewMovie", new[] { "FilmCrew_Id" });
            DropTable("dbo.FilmCrewMovie");
            DropTable("dbo.FilmCrew");
        }
    }
}
