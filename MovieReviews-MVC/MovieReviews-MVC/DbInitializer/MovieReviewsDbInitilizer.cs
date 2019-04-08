using System;
using System.Collections.Generic;
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
  public class MovieReviewsDbInitilizer : CreateDatabaseIfNotExists<ApplicationDbContext>
  {

    protected override void Seed(ApplicationDbContext context)
    {
      base.Seed(context);
      int movieCount = 20;
      int crewCount = 40; //weight actors 0.8 directors 0.2
      int reviewCount = 20;
      int userCount = 30;




      #region UserRoles

      const string adminRoleName = "Admin";
      const string userRoleName = "UserNormal";

      var roleStore = new RoleStore<IdentityRole>(context);
      var roleManager = new RoleManager<IdentityRole>(roleStore);

      var adminRole = new IdentityRole {Name = adminRoleName};
      var userRole = new IdentityRole {Name = userRoleName};

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


      var checkAdminUser = manager.Create(admin, "pass123");
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
          u.AvatarUri = f.Internet.Avatar();
          u.DisplayName = f.Name.FullName();
          u.UserName = userEmail;
          u.Email = userEmail;
          u.EmailConfirmed = true;
        })
        .Generate(userCount);

      users.ForEach(u =>
      {
        var checkUser = manager.Create(u, "pass123");

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

      string[] genreNames =
      {
        "Action", "Action", "Adventure", "Comedy", "Crime", "Drama", "Fantasy", "Horror", "Romance", "Science fiction",
        "Thriller"
      };
      context.Genres.AddRange(genreNames.Select(genre => new Genre() {Title = genre}));

      #endregion

      #region Movies

      var random = new Bogus.Randomizer();
      var reviewGenerator = new Faker<Review>()
        .Rules((f, r) =>
        {
          r.AuthorId = f.PickRandom(users.Select(u => u.Id));
          r.Title = f.Lorem.Sentence();
          r.Body = f.Lorem.Sentences();
          r.CreatedOn = f.Date.Past();
          r.Rating = f.Random.Int(0, 10);
        });

      var movies = new Faker<Movie>()
        .Rules((f, m) =>
        {
          m.Title = f.Hacker.Adjective() + " " + f.Hacker.Noun();
          m.Year = f.Random.Int(2000, 2018);
          m.Length = f.Random.Int(60, 180);
          m.Rating = f.Random.Float(0f, 10f);
          m.Image = f.Image.PicsumUrl(320, 320);
          m.Description = f.Lorem.Sentences();
          m.Reviews = reviewGenerator.Generate(random.Int(3, 8));
          m.Comments = Get20Comments();
          m.Reviews.ForEach(r => r.Comments = Get20Comments());
        })
          .Generate(movieCount);

      //movies = Enumerable.Range(0, movieCount).Zip(movies, (i, m) =>
      //{
      //  m.Reviews.ForEach(r => r.MovieId = i);
      //  m.Id = i;
      //  return m;
      //}).ToList();

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
          c.Comments = Get20Comments();
        })
        .Generate(crewCount);

     
      var moviesArr = movies.ToList();
      crew.ForEach(c => c.Movies = random.ListItems<Movie>(moviesArr, random.Number(3, 10)));

      context.FilmCrewMembers.AddRange(crew);
      #endregion

      List<Comment> Get20Comments()
      {
       
        var comments = new Faker<Comment>()
          .Rules((f, c) =>
          {
            c.AuthorId = f.PickRandom(users.Select(u => u.Id));
            c.CommentBody = f.Lorem.Sentences();
            c.CreatedOn = f.Date.Past();
          })
          .Generate(20);
        var parentCommentsCount = random.Int(8, 13);
        var parentComments = comments.Take(parentCommentsCount).ToArray();
        var childrenComments = comments.Skip(parentCommentsCount).ToArray();
        childrenComments.ForEach(c => c.CommentParentId = random.Number(0, 2));


        return parentComments.Concat(childrenComments).ToList();

      }

      context.Users.ForEach(u => u.EmailConfirmed = true);
      context.SaveChanges();
    }
  }
}