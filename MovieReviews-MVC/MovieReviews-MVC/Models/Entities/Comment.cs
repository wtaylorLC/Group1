using System;

namespace MovieReviews_MVC.Models.Entities
{
  public class Comment    
  {
    public int Id { get; set; }
    public string AuthorId { get; set; }
    public string Body { get; set; }
    public DateTime CreatedOn { get; set; }
    public int  CommentParentId { get; set; }
    public int CommentSubjectId { get; set; }
  }
}