namespace MovieReviews_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationshipsToPostTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movie", "FilmCrewMember_Id", c => c.Int());
            CreateIndex("dbo.Movie", "FilmCrewMember_Id");
            AddForeignKey("dbo.Movie", "FilmCrewMember_Id", "dbo.FilmCrewMember", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movie", "FilmCrewMember_Id", "dbo.FilmCrewMember");
            DropIndex("dbo.Movie", new[] { "FilmCrewMember_Id" });
            DropColumn("dbo.Movie", "FilmCrewMember_Id");
        }
    }
}
