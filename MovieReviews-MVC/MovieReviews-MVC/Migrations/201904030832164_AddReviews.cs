namespace MovieReviews_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "ReviewedMovieId", c => c.Int(nullable: false));
            AlterColumn("dbo.Review", "AuthorId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Review", "AuthorId", c => c.Int(nullable: false));
            DropColumn("dbo.Review", "ReviewedMovieId");
        }
    }
}
