using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.Models;

public class CommunicationStatusChange
{
  public int Id { get; set; }

  [Required(ErrorMessage = "CommunicationId is required.")]
  public int CommunicationId { get; set; }

  public int CommunicationStatusId { get; set; }
  [Required(ErrorMessage = "Status is required.")]
  public CommunicationStatus Status { get; set; } = null!;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
