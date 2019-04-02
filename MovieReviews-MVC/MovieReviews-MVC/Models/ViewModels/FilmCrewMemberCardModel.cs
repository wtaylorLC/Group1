using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieReviews_MVC.Models.ViewModels
{
  public class FilmCrewMemberCardModel
  {
    public int Id { get; set; }
    public string ImageUri { get; set; }
    public string Name { get; set; }
  }
}