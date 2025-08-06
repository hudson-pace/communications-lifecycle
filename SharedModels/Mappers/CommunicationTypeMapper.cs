using SharedModels.DTOs;
using SharedModels.Models;
namespace SharedModels.Mappers;
public static class CommunicationTypeMapper
{
  public static CommunicationTypeDto ToDto(this CommunicationType communicationType) => new()
  {
    Id = communicationType.Id,
    Name = communicationType.Name,
    Statuses = communicationType.Statuses?.Select(status => status.ToDto()).ToList() ?? [],
  };
  public static CommunicationType ToEntity(this CommunicationTypeDto communicationTypeDto) => new()
  {
    Id = communicationTypeDto.Id,
    Name = communicationTypeDto.Name,
    Statuses = communicationTypeDto.Statuses?.Select(status => status.ToEntity()).ToList()!,
  };
  public static void PatchFrom(this CommunicationType communicationType, CommunicationTypeDto dto)
  {
    ArgumentNullException.ThrowIfNull(dto);
    if (dto.Name is not null) communicationType.Name = dto.Name;
    if (dto.Statuses is not null) communicationType.Statuses = [.. dto.Statuses.Select(statusDto => statusDto.ToEntity())];
  }
}