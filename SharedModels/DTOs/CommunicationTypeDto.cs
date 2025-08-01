using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.DTOs;

public class CommunicationTypeDto
{
  public int Id { get; set; }

  [Required(ErrorMessage = "Name is required.")]
  [StringLength(20, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 20 characters.")]
  public string Name { get; set; } = null!;

  public List<CommunicationStatusDto> Statuses { get; set; } = [];
}
