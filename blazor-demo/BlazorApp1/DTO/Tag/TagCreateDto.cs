using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class TagCreateDto
{
  public string Name { get; set; } = null!;
}