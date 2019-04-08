using MovieReviews_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
          var reviews = ctx.Reviews;

            return View();
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
            AuthorName =  author.DisplayName,
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
            CreatedOn = r.CreatedOn.ToString("d"),
            AuthorUsername = ctx.Users.FirstOrDefault(u => u.Id == r.AuthorId).DisplayName
          };
          return card;
        });
        return PartialView("_ReviewCard", vm);
      }

        // GET: Review/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
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
