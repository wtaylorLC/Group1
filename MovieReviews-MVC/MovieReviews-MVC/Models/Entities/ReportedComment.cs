namespace MovieReviews_MVC.Models.Entities
{
  public class ReportedComment
  {
    public int  Id { get; set; }
    public int  CommentId { get; set; }
    public string UserId { get; set; }
    public string Reason { get; set; }
    public bool Resolved { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual Comment Comment { get; set; }

    public ReportedComment()
    {
      Resolved = false;
    }
  }
}