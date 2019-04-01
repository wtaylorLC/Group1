using System.Data.Entity.ModelConfiguration;
using MovieReviews_MVC.Models.Entities;

namespace MovieReviews_MVC.Models.Configurations
{
  internal class MovieEntityDbConfiguration : EntityTypeConfiguration<Movie>
  {
    public MovieEntityDbConfiguration()
    {
      HasMany<Genre>(m => m.Genres)
        .WithMany(g => g.Movies)
        .Map(cs =>
        {
          cs.MapLeftKey("MovieId");
          cs.MapRightKey("GenreId");
          cs.ToTable("MovieGenre");
        });
       
    }
  }
}