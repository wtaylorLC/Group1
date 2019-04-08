using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using MovieReviews_MVC.Models.Entities;

namespace MovieReviews_MVC.Models.ViewModels
{
  public class CommentsViewModel
  {
    public CommentCardViewmodel ParentComment { get; set; }
    public List<CommentCardViewmodel> ChildComments { get; set; }
  }

}