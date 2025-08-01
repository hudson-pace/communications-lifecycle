using System.ComponentModel.DataAnnotations;
using SharedModels.Models;

namespace SharedModels.DTOs;

public class CommunicationDto
{
  public int? Id { get; set; }

  [Required(ErrorMessage = "Name is required.")]
  [StringLength(20, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 20 characters.")]
  public string Title { get; set; } = null!;

  [Required(ErrorMessage = "Communication type is required.")]
  [Range(1, int.MaxValue, ErrorMessage = "Invalid communication type.")]
  public int? CommunicationTypeId { get; set; }
  public CommunicationTypeDto? Type { get; set; } = null!;
  public List<CommunicationStatusChangeDto>? StatusHistory { get; set; } = null!;
}