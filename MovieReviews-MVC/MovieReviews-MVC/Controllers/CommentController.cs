using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MovieReviews_MVC.Models;
using MovieReviews_MVC.Models.Entities;
using MovieReviews_MVC.Models.ViewModels;

namespace MovieReviews_MVC.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
      private ApplicationDbContext ctx;

      public CommentController()
      {
        ctx = new ApplicationDbContext();
      }

      public PartialViewResult CommentCards(int id)
      {
        var comments = ctx.Comments.Where(c => c.CommentType == CommentType.Movie && c.CommentSubjectId == id);
        var topComments = comments.Where(c => c.CommentParentId == null).ToList();
        var replyComments = comments.Where(c => c.CommentParentId != null);

        var vm = topComments.Select(c =>
          new CommentsViewModel
          {
            Id = c.Id,
            AuthorId = c.AuthorId,
            CommentBody = c.CommentBody,
            CreatedOn = c.CreatedOn,
            CommentSubjectId = c.CommentSubjectId,
            ChildComments = replyComments.Where(t => t.CommentParentId == c.Id).ToList(),
          });

        return PartialView("_CommentPartial", vm);
      }
    }
}