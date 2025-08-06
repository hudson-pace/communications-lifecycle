using SharedModels.DTOs;
using SharedModels.Models;
namespace SharedModels.Mappers;
public static class CommunicationStatusChangeMapper
{
  public static CommunicationStatusChangeDto ToDto(this CommunicationStatusChange communicationStatusChange) => new CommunicationStatusChangeDto
  {
    Id = communicationStatusChange.Id,
    CommunicationId = communicationStatusChange.CommunicationId,
    CommunicationStatusId = communicationStatusChange.Status.Id,
    Status = communicationStatusChange.Status?.ToDto(),
    CreatedAt = communicationStatusChange.CreatedAt,
  };
  public static CommunicationStatusChange ToEntity(this CommunicationStatusChangeDto communicationStatusChangeDto) => new CommunicationStatusChange()
  {
    Id = communicationStatusChangeDto.Id,
    CommunicationId = communicationStatusChangeDto.CommunicationId,
    CommunicationStatusId = communicationStatusChangeDto.CommunicationStatusId,
    Status = communicationStatusChangeDto.Status?.ToEntity()!,
    CreatedAt = communicationStatusChangeDto.CreatedAt,
  };
}