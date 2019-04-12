using System.Collections.Generic;
using MovieReviews_MVC.Models.Entities;

namespace MovieReviews_MVC.Models.ViewModels
{
  public class MovieDetailsViewModel
  {
    public int PostId { get; set; }
    public Movie Movie { get; set; }
    public int? ReviewId { get; set; }
    public List<FilmCrewMember> Actors { get; set; }
    public List<FilmCrewMember> Directors { get; set; }
  }
}