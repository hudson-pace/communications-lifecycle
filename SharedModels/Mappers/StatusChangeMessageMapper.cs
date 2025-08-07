using SharedModels.DTOs;
using SharedModels.Models;
namespace SharedModels.Mappers;
public static class StatusChangeMessageMapper
{
  public static CommunicationStatusChangeDto ToCommunicationStatusChangeDto(this StatusChangeMessageDto dto) => new()
  {
    CommunicationId = dto.CommunicationId,
    CommunicationStatusId = dto.CommunicationStatusId,
  };
}