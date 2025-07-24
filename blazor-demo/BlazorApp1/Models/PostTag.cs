using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[PrimaryKey(nameof(PostId), nameof(TagId))]
public class PostTag
{
  public int PostId { get; set; }
  public Post Post { get; set; } = null!;

  public int TagId { get; set; }
  public Tag Tag { get; set; } = null!;
}