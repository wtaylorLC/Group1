using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MovieReviews_MVC.Models;
using MovieReviews_MVC.Models.Entities;

namespace MovieReviews_MVC.Controllers.Api
{
  public class ReviewsController : ApiController
  {
    private ApplicationDbContext _context;

    public ReviewsController()
    {
      _context = new ApplicationDbContext();
    }

    // GET /api/reviews
    public IEnumerable<Review> GetReviews()
    {
      return _context.Reviews.ToList();
    }

    public IEnumerable<Review> GetReview(int id)
    {
      return _context.Reviews.Where(r => r.Id == id);
    }
    [Route("api/{movieId:int}/reviews")]
    public IEnumerable<Review> GetMovieReviews(int movieId)
    {
      return _context.Reviews.Where(r => r.ReviewedMovieId == movieId);
    }
  }
}
