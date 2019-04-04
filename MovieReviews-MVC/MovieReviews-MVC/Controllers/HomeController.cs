using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bogus;
using MovieReviews_MVC.Models;
using MovieReviews_MVC.Models.Entities;
using MovieReviews_MVC.Models.ViewModels;

namespace MovieReviews_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var index = ApplicationDbContext.Create();
            var model = index.Movies.Take(3).Select(m => new MoviesViewModel()
            {
                Id = m.Id, Title = m.Title, Image = m.Image, Rating = m.Rating
            }).ToList();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

    }
}