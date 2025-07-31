using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.DTOs;

public class CommunicationStatusChangeDto
{
  public int Id { get; set; }
  public int CommunicationId { get; set; }
  public int CommunicationStatusId { get; set; }
  public CommunicationStatusDto? Status { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
