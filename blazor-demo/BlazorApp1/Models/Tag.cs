using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Name), IsUnique=true)]
public class Tag
{
  public int Id { get; set; }

  [Required(ErrorMessage = "Name is required.")]
  public string Name { get; set; } = null!;

  public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}