using SharedModels.DTOs;
using SharedModels.Models;

public static class CommunicationTypeMapper
{
  public static CommunicationTypeDto ToDto(this CommunicationType communicationType) => new CommunicationTypeDto
  {
    Id = communicationType.Id,
    Name = communicationType.Name,
  };
  public static CommunicationType ToEntity(this CommunicationTypeDto communicationTypeDto) => new CommunicationType()
  {
    Id = communicationTypeDto.Id,
    Name = communicationTypeDto.Name,
  };
}