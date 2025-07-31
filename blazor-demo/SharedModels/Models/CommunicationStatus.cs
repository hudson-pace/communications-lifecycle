using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.Models;

public class CommunicationStatus
{
  public int Id { get; set; }

  public int CommunicationTypeId { get; set; }
  [Required(ErrorMessage = "Type is required.")]
  public CommunicationType Type { get; set; } = null!;

  [Required(ErrorMessage = "Description is required.")]
  public string Description { get; set; } = null!;
}
