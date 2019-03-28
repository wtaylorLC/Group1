using System.Data.Entity;
using System.Linq;
using MovieReviews_MVC.Models;
using MovieReviews_MVC.Models.Entities;

namespace MovieReviews_MVC.DbInitializer
{
  public class MovieReviewsDbInitilizer : DropCreateDatabaseAlways<ApplicationDbContext>
  {

    protected override void Seed(ApplicationDbContext context)
    {
      #region Users

      #endregion

      #region Gernes

      string[] genreNames = {"Action", "Action", "Adventure", "Comedy", "Crime", "Drama", "Fantasy", "Horror", "Romance", "Science fiction", "Thriller"};
      context.Genres.AddRange(genreNames.Select(genre => new Genre(){Title = genre}));
      #endregion

      #region Movies

      #endregion

      base.Seed(context);
    }
  }
}