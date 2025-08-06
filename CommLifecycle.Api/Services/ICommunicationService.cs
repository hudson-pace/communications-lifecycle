/*
  CRUD for Communication
  status history projection
*/
using System;
using SharedModels.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTOs;

namespace CommLifecycle.Api.Services;

public interface ICommunicationService
{
  Task<List<CommunicationDto>> GetAllCommunicationsAsync();
  Task<CommunicationDto?> GetCommunicationAsync(int id);
  Task<Communication> CreateCommunicationAsync(CommunicationDto CommunicationDto);
  Task<Communication> DeleteCommunicationAsync(Communication Communication);
  Task<Communication> UpdateCommunicationAsync(CommunicationDto communicationDto);
  Task<Communication> UpdateCommunicationStatusAsync(StatusChangeMessageDto statusChangeMessage);

  Task<List<CommunicationTypeDto>> GetAllCommunicationTypesAsync();
  Task<CommunicationTypeDto?> GetCommunicationTypeAsync(int id);
  Task<CommunicationType> CreateCommunicationTypeAsync(CommunicationTypeDto CommunicationTypeDto);
  Task<CommunicationType> UpdateCommunicationTypeAsync(CommunicationTypeDto CommunicationTypeDto);

  Task<CommunicationStatusChange> CreateCommunicationStatusChangeAsync(CommunicationStatusChangeDto CommunicationStatusChangeDto);
}
