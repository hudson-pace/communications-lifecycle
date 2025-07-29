using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.DTOs;

public class CommunicationStatusChangeDto
{
  public int Id { get; set; }
  public CommunicationStatusDto Status { get; set; } = null!;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
