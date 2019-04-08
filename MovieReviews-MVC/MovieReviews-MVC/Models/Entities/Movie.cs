using System.Collections;
using System.Collections.Generic;

namespace MovieReviews_MVC.Models.Entities
{
  public class Movie : Post
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public int Length { get; set; }
    public string Image { get; set; }
    public float Rating { get; set; }



    // Navigation Properties

    public virtual ICollection<Genre> Genres { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<FilmCrewMember> FilmCrewMembers { get; set; }
    
  }
}