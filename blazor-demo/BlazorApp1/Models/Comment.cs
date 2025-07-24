using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models;

public class Comment
{
  public int Id { get; set; }

  [Required(ErrorMessage = "Content is required.")]
  public string Content { get; set; } = null!;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }

  public int Upvotes { get; set; } = 0;
  public int Downvotes { get; set; } = 0;
  public bool IsEdited { get; set; } = false;

  // Author relationship
  public int? AuthorId { get; set; }
  public UserProfile? Author { get; set; }

  [Required(ErrorMessage = "PostId is required.")]
  public int PostId { get; set; }
  [Required(ErrorMessage = "Post is required.")]
  public Post Post { get; set; } = null!;

  public int? ParentCommentId { get; set; }
  public Comment? ParentComment { get; set; }

  public ICollection<Comment> Replies { get; set; } = new List<Comment>();
}
