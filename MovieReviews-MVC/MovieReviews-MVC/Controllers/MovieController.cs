using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bogus;
using Microsoft.AspNet.Identity;
using MovieReviews_MVC.Models;
using MovieReviews_MVC.Models.Entities;
using MovieReviews_MVC.Models.ViewModels;

namespace MovieReviews_MVC.Controllers
{
    public class MovieController : Controller
    {

        private ApplicationDbContext ctx;

        public MovieController()
        {
            ctx = new ApplicationDbContext();
        }
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult MovieCards()
        {

            var vm = ctx.Movies.Select(m => new MoviesViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Image = m.Image,
                Rating = m.Rating
            }).ToList();



            return PartialView("_MovieCards", vm);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            var post = ctx.Posts.Find(id);
            var movie = ctx.Movies.Include(m => m.FilmCrewMembers).FirstOrDefault(m => m.Id == id);
            var filmCrew = movie.FilmCrewMembers;

            int? reviewId = null;
            var userId = User.Identity.GetUserId();
            if (User.Identity.GetUserId() != null)
            {
                var review = ctx.Reviews.FirstOrDefault(r => r.AuthorId == userId && r.MovieId == movie.Id);
                if (review != null)
                {
                    reviewId = review.Id;
                }
            }

            var model = new MovieDetailsViewModel
            {
                PostId = post.Id,
                Movie = movie,
                Directors = filmCrew.Where(c => c.Role == MovieRole.Director).ToList(),
                Actors = filmCrew.Where(c => c.Role == MovieRole.Actor).ToList(),
                ReviewId = reviewId,
                
            };

            return View(model);
        }

        #region Not In Use
        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
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

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
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
        #endregion

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
