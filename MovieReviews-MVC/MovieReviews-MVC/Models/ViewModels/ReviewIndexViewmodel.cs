using System;

namespace MovieReviews_MVC.Models.ViewModels
{
  public class ReviewIndexViewmodel
  {
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public string ReviewBody { get; set; }
    public string ReviewTitle { get; set; }
    public DateTime CreatedOn { get; set; }
    public float Rating { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public string MovieImageUri { get; set; }
  }
}