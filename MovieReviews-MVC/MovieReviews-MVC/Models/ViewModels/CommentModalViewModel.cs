namespace MovieReviews_MVC.Models.ViewModels
{
  public class CommentModalViewModel 
  {
    public string CommentBody { get; set; }
    public int PostId { get; set; }
    public int? CommentParentId { get; set; }
  }
}