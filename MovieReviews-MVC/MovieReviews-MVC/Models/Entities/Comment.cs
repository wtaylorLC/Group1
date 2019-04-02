using System;

namespace MovieReviews_MVC.Models.Entities
{
  public class Comment    
  {
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Body { get; set; }
    public DateTime CreatedOn { get; set; }
    public int  CommentParentId { get; set; }
  }
}