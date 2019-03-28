using System.Collections.Generic;

namespace MovieReviews_MVC.Models.Entities
{
  public class Genre
  {
    public int Id { get; set; }
    public string Title { get; set; }

    public ICollection<Movie> Movies { get; set; }
  }
}