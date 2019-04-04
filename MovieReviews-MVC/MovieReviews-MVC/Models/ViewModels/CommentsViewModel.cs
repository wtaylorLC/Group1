using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using MovieReviews_MVC.Models.Entities;

namespace MovieReviews_MVC.Models.ViewModels
{
  public class CommentsViewModel
  {
    public int Id { get; set; }
    public CommentType CommentType { get; set; }
    public string AuthorId { get; set; }
    public string CommentBody { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? CommentParentId { get; set; }
    public int CommentSubjectId { get; set; }

    public List<Comment> ChildComments { get; set; }
  }

  //public class FakeComment
  //{
  //  public int Id { get; set; }
  //  public CommentType CommentType { get; set; }
  //  public int CommentedSubjectId { get; set; }
  //  public string Username { get; set; }
  //  public string CommentBody { get; set; }
  //  public int? ParentCommentId { get; set; }
  //  public DateTime DateCreated { get; set; }
  //  public string AvatarUri { get; set; }
  //  public List<FakeComment> FakeComments { get; set; }
  //}
}