using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieReviews_MVC.Models.ViewModels
{
    public class ReviewModalViewModel
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        [DisplayName("Headline")]
        [StringLength(80, MinimumLength = 3)]
        public string Title { get; set; }


        [Required]
        [StringLength(1000, MinimumLength = 3)]
        [DisplayName("Review")]
        public string Body { get; set; }
        public float Rating { get; set; }
    }
}