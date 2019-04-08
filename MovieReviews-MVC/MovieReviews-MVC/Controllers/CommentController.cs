using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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
        var comments = ctx.Comments.Where(c => c.Post.Id ==  id);
        var userIds = comments.Select(c => c.AuthorId);
        var users = ctx.Users.Where(u => userIds.Contains(u.Id)).ToDictionary(u => u.Id, u => new List<string>(){u.DisplayName, u.AvatarUri});

        Func<Comment, CommentCardViewmodel> convertToCardVM = c => new CommentCardViewmodel()
        {
          Id = c.Id,
          AuthorDisplayName = users[c.AuthorId][0],
          AvatarUri = users[c.AuthorId][1],
          CommentBody = c.CommentBody,
          CreatedOn = c.CreatedOn.ToString("d"),
          ParentCommentId = c.CommentParentId,
        };
        var parentComments = comments.Where(c => c.CommentParentId == null).ToList();
        var childComments = comments.Where(c => c.CommentParentId != null).Select(convertToCardVM);
        
        var vm = parentComments.Select(c => new CommentsViewModel()
        {
          ParentComment = convertToCardVM(c),
          ChildComments = childComments.Where(cm => cm.ParentCommentId == c.Id).ToList(),
        }).ToArray();

        return PartialView("_CommentPartial", vm);
      }

       [HttpGet] 
      public ActionResult GetReportModal(int id)
      {
        var comment = ctx.Comments.FirstOrDefault(c => c.Id == id);

        
        return PartialView("_ReportComment", new ReportCommentViewModel(){Id = comment.Id});
      }

       [HttpPost]
       [ValidateAntiForgeryToken]
       [Authorize(Roles = "UserNormal")]
      public ActionResult Report(ReportCommentViewModel model)
       {
        var reportedComment = new ReportedComment()
        {
          CommentId = model.Id,
          UserId = User.Identity.GetUserId(),
          Reason = model.Reason,
        };

         try
         {
          ctx.ReportedComments.Add(reportedComment);
          ctx.SaveChanges();
          }
         catch (Exception e)
         {
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
         }

        

          return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}