using System;

namespace MovieReviews_MVC.Models.Entities
{
  public class Article : Post
  {
    public int AuthorId { get; set; }
    public string Body { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Title { get; set; }
  }
}