using System.Collections;
using System.Collections.Generic;

namespace MovieReviews_MVC.Models.Entities
{
  public class Movie
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public int Length { get; set; }
    public string Image { get; set; }
    public int Rating { get; set; }



    // Navigation Properties

    public virtual ICollection<Genre> Genres { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<FilmCrewMember> FilmCrewMembers { get; set; }
    //public virtual ICollection<Comment> Comments { get; set; }
    
  }
}