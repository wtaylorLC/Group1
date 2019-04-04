using System;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using Bogus;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieReviews_MVC.Models;
using MovieReviews_MVC.Models.Entities;
using Newtonsoft.Json;

namespace MovieReviews_MVC.DbInitializer
{
  public class MovieReviewsDbInitilizer : DropCreateDatabaseAlways<ApplicationDbContext>
  {

    protected override void Seed(ApplicationDbContext context)
    {
      // int userCount = 0;
      int movieCount = 20;
      int crewCount = 40; //weight actors 0.8 directors 0.2
      int reviewCount = 20;
      int userCount = 30;

      #region UserRoles

      const string adminRoleName = "Admin";
      const string userRoleName = "UserNormal";

        var roleStore = new RoleStore<IdentityRole>(context);
        var roleManager = new RoleManager<IdentityRole>(roleStore);

        var adminRole = new IdentityRole { Name = adminRoleName };
        var userRole = new IdentityRole { Name = userRoleName };

        roleManager.Create(adminRole);
        roleManager.Create(userRole);


      #endregion
      
      #region Users

      var store = new UserStore<ApplicationUser>(context);
      var manager = new UserManager<ApplicationUser>(store);

      #region Admin user account

      var admin = new ApplicationUser()
      {
        Id = Guid.NewGuid().ToString(),
        UserName = "admin@gmail.com",
        Email = "admin@gmail.com",
        EmailConfirmed = true
      };


      var checkAdminUser = manager.Create(admin, "@Password123");
      if (checkAdminUser.Succeeded)
      {
        manager.AddToRole(admin.Id, adminRoleName);
      }
      else
      {
        var errors = checkAdminUser.Errors;
        errors.ForEach(e => Debug.WriteLine(e));
      }
      

      #endregion

      var users = new Faker<ApplicationUser>()
        .Rules((f, u) =>
        {
          var userEmail = f.Internet.Email();
            u.Id = Guid.NewGuid().ToString();
            u.UserName = userEmail;
            u.Email = userEmail;
            u.EmailConfirmed = true;
          })
        .Generate(userCount);

      users.ForEach(u =>
      {
        var checkUser = manager.Create(u, "@Password123");

        if (checkUser.Succeeded)
        {
          manager.AddToRole(u.Id, userRoleName);
        }
        else
        {
          checkUser.Errors.ForEach(e => Debug.WriteLine(e));
        }
        
      });


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
        .Generate(movieCount);

      context.Movies.AddRange(movies);
      #endregion

      #region FilmCrewMember
      var crew = new Faker<FilmCrewMember>()
        .Rules((f, c) =>
        {
          c.Bio = f.Lorem.Sentences();
          c.DoB = f.Date.Past(60, new DateTime(1990, 2, 15));
          c.ImageUri = f.Internet.Avatar();
          c.Name = f.Name.FullName();
          c.Role = (MovieRole)f.Random.WeightedRandom(new int[] { 0, 1 }, new float[]
          {
            0.8f, 0.2f
          });

        })
        .Generate(crewCount);

      var random = new Bogus.Randomizer();
      var moviesArr = movies.ToArray();
      crew.ForEach(c => c.Movies = random.ArrayElements<Movie>(moviesArr, random.Number(3, 10)));

      context.FilmCrewMembers.AddRange(crew);
      #endregion

      #region Reviews

      // Movie Ids - auto incremented, ids will start at 0 to number of movies - 1
      var movieIds = Enumerable.Range(0, movieCount - 1).ToArray();

      var reviews = new Faker<Review>()
        .Rules((f, r) =>
        {
          r.AuthorId = f.PickRandom(users.Select(u => u.Id));
          r.ReviewedMovieId = f.Random.Number(0, movieCount - 1);
          r.Title = f.Lorem.Sentence();
          r.Body = f.Lorem.Sentences();
          r.CreatedOn = f.Date.Past();
          r.Rating = f.Random.Int(0, 10);
        })
        .Generate(reviewCount);

      context.Reviews.AddRange(reviews);
      #endregion

      #region Comments
      var comments = new Faker<Comment>()
        .Rules((f, c) =>
        {
          c.AuthorId = f.PickRandom(users.Select(u => u.Id));
          c.CommentBody = f.Lorem.Sentences();
          c.CommentSubjectId = 3;
          c.CommentType = CommentType.Movie;
          c.CreatedOn = f.Date.Past();
        })
        .Generate(30);

      var parentComments = comments.Take(8).ToArray();
      var childrenComments = comments.Skip(8).ToArray();
      childrenComments.ForEach(c => c.CommentParentId = random.Number(0, 7));

      context.Comments.AddRange(parentComments.Concat(childrenComments));

      #endregion

      context.Users.ForEach(u => u.EmailConfirmed =true);

      base.Seed(context);
    }
  }
}