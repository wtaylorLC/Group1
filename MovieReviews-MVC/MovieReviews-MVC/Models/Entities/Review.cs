﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieReviews_MVC.Models.Entities
{
  public class Review
  {
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int  Rating { get; set; }
    public string Body { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Title { get; set; }


    // Navigation Properties
    //public virtual ICollection<Comment> Comments { get; set; }
  }
}