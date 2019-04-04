using MovieReviews_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public JsonResult MovieReviews(int id)
        {
            var reviews = ctx.Reviews.Where(r => r.ReviewedMovieId == id);
            return Json(reviews);
        }
        // GET: Review/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
