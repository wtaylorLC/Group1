using System.Data.Entity.ModelConfiguration;
using MovieReviews_MVC.Models.Entities;

namespace MovieReviews_MVC.Models.Configurations
{
  public class CommentEntityDbConffiguration : EntityTypeConfiguration<Comment>
  {
    public CommentEntityDbConffiguration()
    {
     
    }    
  }
}