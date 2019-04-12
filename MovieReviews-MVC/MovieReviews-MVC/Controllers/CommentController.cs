using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        #region Fields
        // GET: Comment
        private ApplicationDbContext ctx;
        #endregion

        #region Constructor
        public CommentController()
        {
            ctx = new ApplicationDbContext();
        }
        #endregion

        #region Get Comments for Post
        public PartialViewResult CommentCards(int id)
        {
            var comments = ctx.Comments.Where(c => c.Post.Id == id);
            var userIds = comments.Select(c => c.AuthorId);
            var users = ctx.Users
                .Where(u => userIds.Contains(u.Id))
                .ToDictionary(u =>
                {
                    Debug.WriteLine(u.Id);
                    return u.Id;
                }, u =>
                {
                    Debug.WriteLine(u.DisplayName, u.AvatarUri);
                    return new List<string>() {u.DisplayName, u.AvatarUri};
                });

            Func<Comment, CommentCardViewmodel> convertToCardVM = c => new CommentCardViewmodel()
            {
                Id = c.Id,
                AuthorDisplayName = users[c.AuthorId][0],
                AvatarUri = users[c.AuthorId][1],
                CommentBody = c.CommentBody,
                CreatedOn = c.CreatedOn.ToString("d"),
                ParentCommentId = c.CommentParentId,
                PostId = c.PostId,
            };
            var parentComments = comments.Where(c => c.CommentParentId == null).ToList();
            var childComments = comments.Where(c => c.CommentParentId != null).ToList().Select(convertToCardVM);

            var vm = parentComments.Select(c => new CommentsViewModel()
            {
                ParentComment = convertToCardVM(c),
                ChildComments = childComments.Where(cm => cm.ParentCommentId == c.Id).ToList(),
            }).ToArray();

            return PartialView("_CommentPartial", vm);
        }
        #endregion

        #region Report inapropriate comment actions

        [HttpGet]
        [Authorize]
        public ActionResult GetReportModal(int id)
        {
            var comment = ctx.Comments.FirstOrDefault(c => c.Id == id);


            return PartialView("_ReportComment", new ReportCommentViewModel() { Id = comment.Id });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
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
        #endregion

        #region Comment on post actions

        [HttpGet]
        [Route("Comment/Add/{postId:int}/{commentId:int?}")]
        public ActionResult CommentModal(int postId, int? commentId)
        {
            // postId + no comment id => add it to main comment thread
            // postId + commentId, but comment has no parent comment id => add it as a child
            // postId + commentId, comment has parent comment id => add it as a child under same parent

            // Find post
            var post = ctx.Posts.FirstOrDefault(p => p.Id == postId);

            if (post == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           
            // If we have commentId, find comment
            Comment comment = null;
            if (commentId != null)
            {
                comment = ctx.Comments.FirstOrDefault(c => c.Id == commentId);

                if (comment == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                return PartialView("_CommentModal", new CommentModalViewModel
                {
                    PostId = post.Id,
                    CommentParentId = comment.CommentParentId ?? comment.Id
                });
            }
             
            // if we have comment and/or post, create viewmodel for comment modal
            var vm = new CommentModalViewModel
            {
                PostId = post.Id,
                CommentParentId = null,
            };

            return PartialView("_CommentModal", vm);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentModalViewModel model)
        {
            var comment = new Comment
            {
                AuthorId = User.Identity.GetUserId(),
                CommentBody = model.CommentBody,
                CreatedOn = DateTime.Now,
                PostId = model.PostId,
                CommentParentId = model.CommentParentId,
            };
            try
            {
                ctx.Comments.Add(comment);
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        #endregion


    }
}