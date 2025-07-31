using SharedModels.DTOs;
using SharedModels.Models;

public static class CommunicationMapper
{
  public static CommunicationDto ToDto(this Communication communication) => new CommunicationDto
  {
    Id = communication.Id,
    Title = communication.Title,
    Type = communication.Type?.ToDto(),
    StatusHistory = communication.StatusHistory?.Select(statusChange => statusChange.ToDto()).ToList(),
  };
  public static Communication ToEntity(this CommunicationDto communicationDto) => new Communication()
  {
    Id = communicationDto.Id,
    Title = communicationDto.Title,
    Type = communicationDto.Type?.ToEntity()!,
    StatusHistory = communicationDto.StatusHistory?.Select(statusChange => statusChange.ToEntity()).ToList()!,
  };
}