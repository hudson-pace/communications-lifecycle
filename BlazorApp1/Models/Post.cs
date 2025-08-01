using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models;

public class Post
{
  public int Id { get; set; }

  [Required(ErrorMessage = "Title is required.")]
  public string Title { get; set; } = string.Empty;

  [Required(ErrorMessage = "Content is required.")]
  public string Content { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }
  public int Upvotes { get; set; } = 0;
  public int Downvotes { get; set; } = 0;
  public bool IsEdited { get; set; } = false;

  // Author relationship.
  public int? AuthorId { get; set; }
  public UserProfile? Author { get; set; }

  public ICollection<Comment> Comments { get; set; } = new List<Comment>();
  public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}
