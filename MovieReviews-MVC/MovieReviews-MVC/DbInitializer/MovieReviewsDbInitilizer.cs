using System;
using System.Data.Entity;
using System.Linq;
using Bogus;
using MovieReviews_MVC.Models;
using MovieReviews_MVC.Models.Entities;
using Newtonsoft.Json;

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
        .RuleFor(m => m.Title, f => f.Hacker.Adjective()+" " + f.Hacker.Noun())
        .RuleFor(m => m.Year, f => f.Random.Int(2000, 2018))
        .RuleFor(m => m.Length, f => f.Random.Int(60, 180))
        .RuleFor(m => m.Rating, f => f.Random.Int(0, 10))
        .RuleFor(m => m.Image, f => f.Image.PicsumUrl(320, 320))
        .RuleFor(m => m.Description, f => f.Lorem.Sentences())
        .Generate(20);

      context.Movies.AddRange(movies);
      #endregion

      #region FilmCrewMember
      var crew = new Faker<FilmCrewMember>()
        .Rules((f, c) =>
        {
          c.Id = f.IndexFaker;
          c.Bio = f.Lorem.Sentences();
          c.DoB = f.Date.Past(60, new DateTime(1990, 2, 15));
          c.ImageUri = f.Internet.Avatar();
          c.Name = f.Name.FullName();
          c.Role = (MovieRole)f.Random.WeightedRandom(new int[] { 0, 1 }, new float[]
          {
            0.8f, 0.2f
          });

        }).Generate(40);
      var random = new Bogus.Randomizer();
      var moviesArr = movies.ToArray();
      crew.ForEach(c => c.Movies = random.ArrayElements<Movie>(moviesArr, random.Number(3, 10)));

      context.FilmCrewMembers.AddRange(crew);
      #endregion

      base.Seed(context);
    }
  }
}