using SharedModels.Models;

namespace SharedModels.DTOs;

public class CommunicationStatusDto
{
  public int Id { get; set; }
  public int CommunicationTypeId { get; set; }
  public CommunicationTypeDto? Type { get; set; }

  public string Description { get; set; } = null!;
}