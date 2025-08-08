using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.Models;

public class CommunicationStatusChange
{
  public int Id { get; set; }

  [Required(ErrorMessage = "CommunicationId is required.")]
  public int CommunicationId { get; set; }
  [Required(ErrorMessage="Status foreign key (CommunicationStatusId) is required.")]
  public int CommunicationStatusId { get; set; }
  public CommunicationStatus Status { get; set; } = null!;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
