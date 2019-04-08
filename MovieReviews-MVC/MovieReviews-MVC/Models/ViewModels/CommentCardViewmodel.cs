namespace MovieReviews_MVC.Models.ViewModels
{
  public class CommentCardViewmodel
  {
    public int Id { get; set; }
    public string AuthorDisplayName { get; set; }
    public string CommentBody { get; set; }
    public string CreatedOn { get; set; }
    public string AvatarUri { get; set; }
    public int? ParentCommentId { get; set; }
  }
}