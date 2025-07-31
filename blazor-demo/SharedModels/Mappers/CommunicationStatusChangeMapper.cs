using SharedModels.DTOs;
using SharedModels.Models;

public static class CommunicationStatusChangeMapper
{
  public static CommunicationStatusChangeDto ToDto(this CommunicationStatusChange communicationStatusChange) => new CommunicationStatusChangeDto
  {
    Id = communicationStatusChange.Id,
    CommunicationId = communicationStatusChange.Communication.Id,
    CommunicationStatusId = communicationStatusChange.Status.Id,
    Status = communicationStatusChange.Status?.ToDto(),
    CreatedAt = communicationStatusChange.CreatedAt,
  };
  public static CommunicationStatusChange ToEntity(this CommunicationStatusChangeDto communicationStatusChangeDto) => new CommunicationStatusChange()
  {
    Id = communicationStatusChangeDto.Id,
    CommunicationId = communicationStatusChangeDto.CommunicationId,
    Status = communicationStatusChangeDto.Status?.ToEntity()!,
    CreatedAt = communicationStatusChangeDto.CreatedAt,
  };
}