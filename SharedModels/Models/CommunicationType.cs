using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.Models;

public class CommunicationType
{
  public int Id { get; set; }

  [Required(ErrorMessage = "Name is required.")]
  public string Name { get; set; } = null!;
  [MinLength(1)]
  public ICollection<CommunicationStatus> Statuses { get; set; } = [];

}
