using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieReviews_MVC.Models.Entities
{
  public class Review : Post
  {
    public string AuthorId { get; set; }
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime CreatedOn { get; set; }
    public float  Rating { get; set; }

    public virtual Movie Movie { get; set; }
  
  }
}