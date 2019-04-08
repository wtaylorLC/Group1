using System;
using System.Collections.Generic;


namespace MovieReviews_MVC.Models.Entities
{
  public class Comment    
  {
    public int Id { get; set; }
    public string AuthorId { get; set; }
    public string CommentBody { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? CommentParentId { get; set; }

    public virtual Post Post { get; set; }
    public virtual IEnumerable<ReportedComment> ReportedComments { get; set; }
  }
}
