using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using MovieReviews_MVC.Models.Entities;

namespace MovieReviews_MVC.Models.ViewModels
{
  public class CommentsViewModel
  {
    public List<FakeComment> FakeComments;
    public Movie Movie { get; set; }
    public List<FilmCrewMember> Directors { get; set; }
    public List<FilmCrewMember> Actors { get; set; }
    
    public CommentsViewModel()
    {
      FakeComments = MakeComments(5, 5);
      FakeComments.Sort((c1, c2) => DateTime.Compare(c1.DateCreated, c2.DateCreated));
      FakeComments.ForEach(f =>
      {
        var childFakeComments = MakeComments(1, 4, f.Id);
        childFakeComments.Sort((c1, c2) => DateTime.Compare(c1.DateCreated, c2.DateCreated));
        f.FakeComments = childFakeComments;
      });
    }

    private List<FakeComment> MakeComments(int min, int max, int? parentId = null)
    {
      var random = new Bogus.Randomizer();
      return new Faker<FakeComment>()
        .RuleFor(c => c.Id, f => f.UniqueIndex)
        .RuleFor(c => c.CommentType, f => CommentType.Movie)
        .RuleFor(c => c.CommentedSubjectId, f=> 3)
        .RuleFor(c => c.Username, f => f.Name.FullName())
        .RuleFor(c => c.CommentBody, f => f.Lorem.Sentences())
        .RuleFor(c => c.ParentCommentId, f => parentId)
        .RuleFor(c => c.DateCreated, f => f.Date.Past())
        .RuleFor(c => c.AvatarUri, f => f.Internet.Avatar())
        .Generate(random.Number(min, max)).ToList();
    }
  }

  public class FakeComment
  {
    public int Id { get; set; }
    public CommentType CommentType { get; set; }
    public int CommentedSubjectId { get; set; }
    public string Username { get; set; }
    public string CommentBody { get; set; }
    public int? ParentCommentId { get; set; }
    public DateTime DateCreated { get; set; }
    public string AvatarUri { get; set; }
    public List<FakeComment> FakeComments { get; set; }
  }

  public enum CommentType: byte
  {
    Movie,
    Review,
    Actor,
    Director,
    NewsArticle
  }
}