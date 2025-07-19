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
  public string AuthorId { get; set; } = string.Empty;
  public UserProfile Author { get; set; } = null!;

  // Post relationship
  public int PostId { get; set; }
  public Post Post { get; set; } = null!;

  // Replies to this comment.
  public List<Comment> Replies { get; set; } = new();
}
