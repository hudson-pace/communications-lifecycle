using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Models;

public class UserProfile
{
  public int UserProfileId { get; set; }
  public string? Sub { get; set; }
  public string? FavoriteColor { get; set; }

  public ICollection<Post> Posts { get; set; } = new List<Post>();
  public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
