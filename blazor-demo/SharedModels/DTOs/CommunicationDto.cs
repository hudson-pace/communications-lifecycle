using SharedModels.Models;

namespace SharedModels.DTOs;

public class CommunicationDto
{
  public int Id { get; set; }

  public string Title { get; set; } = null!;

  public int CommunicationTypeId { get; set; }
  public CommunicationTypeDto? Type { get; set; } = null!;
  public List<CommunicationStatusChangeDto>? StatusHistory { get; set; } = null!;
}