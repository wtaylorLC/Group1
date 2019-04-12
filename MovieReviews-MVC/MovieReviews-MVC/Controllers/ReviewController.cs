using MovieReviews_MVC.Models;
using System.Data.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MovieReviews_MVC.Models.Entities;
using MovieReviews_MVC.Models.ViewModels;

namespace MovieReviews_MVC.Controllers
{
    public class ReviewController : Controller
    {
        private ApplicationDbContext ctx;

        public ReviewController()
        {
            ctx = new ApplicationDbContext();
        }
        // GET: Review
        public ActionResult Index()
        {
            var reviews = ctx.Reviews.Include(r => r.Movie).ToList();
            var authorIdsForReviews = reviews.Select(r => r.AuthorId).Distinct().ToList();
            var authorInfo = ctx.Users.Where(u => authorIdsForReviews.Contains(u.Id))
                .ToDictionary(u => u.Id, u => u.DisplayName);
            var vm = reviews.Select(r => new ReviewCardWithMovieViewModel()
            {
                Id = r.Id,
                AuthorDisplayName = authorInfo[r.AuthorId],
                Title = r.Title,
                ReviewRating = r.Rating,
                CreatedOn = r.CreatedOn.ToString("dd MMMM yyyy"),
                MovieTitle = r.Movie.Title,
            }).ToList();

            return View(vm);
        }

        // GET: Review/Details/5
        public ActionResult Details(int id)
        {
            var review = ctx.Reviews.FirstOrDefault(r => r.Id == id);
            var movie = ctx.Movies.FirstOrDefault(m => review.MovieId == m.Id);
            var author = ctx.Users.FirstOrDefault(u => review.AuthorId == u.Id);

            var vm = new ReviewIndexViewmodel
            {
                Id = review.Id,
                AuthorName = author.DisplayName,
                CreatedOn = review.CreatedOn,
                MovieId = movie.Id,
                MovieTitle = movie.Title,
                Rating = review.Rating,
                ReviewBody = review.Body,
                ReviewTitle = review.Title,
                MovieImageUri = movie.Image

            };
            return View(vm);
        }
        //[Route("/Review/Reviews/{id:int}")]
        public PartialViewResult Reviews(int id)
        {
            var reviews = ctx.Reviews.Where(r => r.MovieId == id).ToArray();

            var vm = reviews.Select(r =>
            {
                var card = new ReviewCardViewModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Body = r.Body,
                    Rating = r.Rating,
                    CreatedOn = r.CreatedOn.ToString("dd MMMM yyyy"),
                    AuthorUsername = ctx.Users.FirstOrDefault(u => u.Id == r.AuthorId).DisplayName
                };
                return card;
            });
            return PartialView("_ReviewCard", vm);
        }

        // GET: Review/ReviewModal
        public ActionResult ReviewModal(int id)
        {
            // Check if user has reviewed this movie
            var userId = User.Identity.GetUserId();
            var review = ctx.Reviews.FirstOrDefault(r => r.MovieId == id && r.AuthorId == userId);

            if (review != null)
            {
                return RedirectToAction("Details", "Movie", new { id = review.MovieId });
            }

            return PartialView("_ReviewModal", new ReviewModalViewModel{ MovieId = id});
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Create(ReviewModalViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    ctx.Reviews.Add(new Review()
                    {
                        AuthorId = User.Identity.GetUserId(),
                        Body = model.Body,
                        CreatedOn = DateTime.Now,
                        MovieId = model.MovieId,
                        Rating = model.Rating,
                        Title = model.Title
                    });
                    ctx.SaveChanges();

                    //return RedirectToAction("Details", "Movie", new {id = model.MovieId});
                    return Redirect(Request.UrlReferrer.ToString());
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Review/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
