using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.Models;

public class CommunicationStatusChange
{
  public int Id { get; set; }

  public int CommunicationId { get; set; }
  [Required(ErrorMessage = "Communication is required.")]
  public Communication Communication { get; set; } = null!;

  public int CommunicationStatusId { get; set; }
  [Required(ErrorMessage = "Status is required.")]
  public CommunicationStatus Status { get; set; } = null!;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
