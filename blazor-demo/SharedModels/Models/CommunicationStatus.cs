using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.Models;

public class CommunicationStatus
{
  public int Id { get; set; }

  [Required(ErrorMessage = "CommunicationTypeId is required.")]
  public int CommunicationTypeId { get; set; }

  [Required(ErrorMessage = "Description is required.")]
  public string Description { get; set; } = null!;
}
