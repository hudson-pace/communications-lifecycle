using System;
using System.ComponentModel.DataAnnotations;
using BlazorApp1.Models;

namespace BlazorApp1.DTO;

public class PostCreateDto
{
  [Required(ErrorMessage = "Title is required.")]
  public string Title { get; set; } = string.Empty;

  [Required(ErrorMessage = "Content is required.")]
  public string Content { get; set; } = string.Empty;

  [Required(ErrorMessage = "PostTags is required")]
  public List<string> PostTags { get; set; } = new List<string>();
  public int AuthorId { get; set; }
}