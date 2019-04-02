using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieReviews_MVC.Models.Entities
{
  public class FilmCrewMember
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    // TODO Move to ViewModel
    [Display(Name = "Date of Birth:")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime DoB { get; set; }
    public string ImageUri { get; set; }
    public MovieRole Role { get; set; }

    // Navigation Property
    public virtual ICollection<Movie> Movies { get; set; }
    //public virtual ICollection<Comment> Comments { get; set; }
    //public virtual ICollection<Article> Articles { get; set; }
  }
}