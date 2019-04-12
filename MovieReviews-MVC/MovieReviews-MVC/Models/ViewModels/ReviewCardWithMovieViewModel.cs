namespace MovieReviews_MVC.Models.ViewModels
{
    public class ReviewCardWithMovieViewModel
    {
        public int Id { get; set; }
        public string AuthorDisplayName { get; set; }
        public string Title { get; set; }
        public float ReviewRating { get; set; }
        public string CreatedOn { get; set; }
        public string MovieTitle { get; set; }
        
    }
}