using System.Collections.Generic;

namespace MovieReviews_MVC.Models.Entities
{
    public abstract class Post
    {
        public int Id { get; set; }
        
        // Navigation Properties
        public virtual ICollection<Comment> Comments { get; set; }
    }
}