using SharedModels.DTOs;
using SharedModels.Models;

public static class CommunicationStatusMapper
{
  public static CommunicationStatusDto ToDto(this CommunicationStatus communicationStatus) => new CommunicationStatusDto
  {
    Id = communicationStatus.Id,
    CommunicationTypeId = communicationStatus.CommunicationTypeId,
    Description = communicationStatus.Description,
  };
  public static CommunicationStatus ToEntity(this CommunicationStatusDto communicationStatusDto) => new CommunicationStatus()
  {
    Id = communicationStatusDto.Id,
    CommunicationTypeId = communicationStatusDto.CommunicationTypeId,
    Description = communicationStatusDto.Description,
  };
}