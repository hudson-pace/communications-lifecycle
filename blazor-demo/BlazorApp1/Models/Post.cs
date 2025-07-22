using System;

namespace BlazorApp1.Models;

public class Post
{
  public int Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }
  public int Upvotes { get; set; }
  public int Downvotes { get; set; }
  public bool IsEdited { get; set; }
  public List<string> Tags { get; set; } = new();

  // Author relationship.
  public int? AuthorId { get; set; }
  public UserProfile? Author { get; set; }

  public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
