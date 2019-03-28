using System.Data.Entity;
using System.Linq;
using Bogus;
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

      var movies = new Faker<Movie>()
        .RuleFor(m => m.Title, f => f.Hacker.Adjective() + f.Hacker.Noun())
        .RuleFor(m => m.Year, f => f.Random.Int(2000, 2018))
        .RuleFor(m => m.Length, f => f.Random.Int(60, 180))
        .RuleFor(m => m.Rating, f => f.Random.Float(-5, 5))
        .RuleFor(m => m.Image, f => f.Image.PicsumUrl(320, 320))
        .RuleFor(m => m.Description, f => f.Lorem.Text())
        .Generate(20);

      context.Movies.AddRange(movies);
      #endregion

      base.Seed(context);
    }
  }
}