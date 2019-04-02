using System.Data.Entity.ModelConfiguration;
using MovieReviews_MVC.Models.Entities;

namespace MovieReviews_MVC.Models.Configurations
{
  internal class GenreEntityDbConfiguration : EntityTypeConfiguration<Genre>
  {
    public GenreEntityDbConfiguration()
    {
      
    }
  }
}