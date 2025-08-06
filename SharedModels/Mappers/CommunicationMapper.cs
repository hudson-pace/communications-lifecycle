using SharedModels.DTOs;
using SharedModels.Models;
namespace SharedModels.Mappers;

public static class CommunicationMapper
{
  public static CommunicationDto ToDto(this Communication communication) => new CommunicationDto
  {
    Id = communication.Id,
    Title = communication.Title,
    CommunicationTypeId = communication.CommunicationTypeId,
    Type = communication.Type?.ToDto(),
    StatusHistory = communication.StatusHistory?.Select(statusChange => statusChange.ToDto()).ToList(),
  };
  public static Communication ToEntity(this CommunicationDto communicationDto) => new Communication()
  {
    Id = communicationDto.Id ?? 0,
    Title = communicationDto.Title,
    CommunicationTypeId = communicationDto.CommunicationTypeId ?? 0,
    Type = communicationDto.Type?.ToEntity()!,
    StatusHistory = communicationDto.StatusHistory?.Select(statusChange => statusChange.ToEntity()).ToList()!,
  };
  public static void PatchFrom(this Communication communication, CommunicationDto dto)
  {
    ArgumentNullException.ThrowIfNull(dto);
    if (dto.Title is not null) communication.Title = dto.Title;
  }
}