﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieReviews_MVC.Models.Configurations;
using MovieReviews_MVC.Models.Entities;

namespace MovieReviews_MVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
        modelBuilder.Configurations.Add(new GenreEntityDbConfiguration());
        modelBuilder.Configurations.Add(new MovieEntityDbConfiguration());

        base.OnModelCreating(modelBuilder);
    }

      public DbSet<Genre> Genres { get; set; }
      public DbSet<Movie> Movies { get; set; }
    }
}