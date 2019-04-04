namespace MovieReviews_MVC.Models.ViewModels
{
  public class ReviewCardViewModel
  {
    public int Id { get; set; }
    public string AuthorUsername { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string CreatedOn { get; set; }
  }
}