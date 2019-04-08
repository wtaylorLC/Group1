using System.Web.UI.WebControls;

namespace MovieReviews_MVC.Models.ViewModels
{
  public class MoviesViewModel
  {
    public int Id { get; set; }
    public string Title  { get; set; }
    public string Image { get; set; }
    public float Rating { get; set; }
  }
}