using System.Collections;
using System.Collections.Generic;

namespace MovieReviews_MVC.Models.Entities
{
  public class Movie
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int Length { get; set; }
    public string Image { get; set; }
    public float Rating { get; set; }



    // Navigation Properties

    // FilmCrew
    public virtual ICollection<Genre> Genres { get; set; }
    // Revies
    // Ratings
  }
}