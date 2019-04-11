using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MovieReviews_MVC.Models.Entities;

namespace MovieReviews_MVC.Models.ViewModels
{
    public class FillmCrewMemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        [Display(Name = "Date of Birth:")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DoB { get; set; }
        public string ImageUri { get; set; }
        public MovieRole Role { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}