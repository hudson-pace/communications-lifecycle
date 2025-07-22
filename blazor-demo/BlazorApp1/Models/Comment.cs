using System;

namespace BlazorApp1.Models;

public class Comment
{
  public int Id { get; set; }
  public string Content { get; set; } = string.Empty;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }

  public int Upvotes { get; set; }
  public int Downvotes { get; set; }

  public bool IsEdited { get; set; }

  // Author relationship
  public int? AuthorId { get; set; }
  public UserProfile Author { get; set; } = null!;

  // Post relationship
  public int? PostId { get; set; }
  public Post Post { get; set; } = null!;

}
