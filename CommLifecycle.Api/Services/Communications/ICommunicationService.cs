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
  Task<Result<List<CommunicationDto>>> GetAllAsync(CancellationToken ct);
  Task<Result<CommunicationDto>> GetByIdAsync(int id, CancellationToken ct);
  Task<Result<CommunicationDto>> CreateAsync(CommunicationDto CommunicationDto, CancellationToken ct);
  Task<Result<CommunicationDto>> DeleteAsync(int id, CancellationToken ct);
  Task<Result<CommunicationDto>> UpdateAsync(int id, CommunicationDto communicationDto, CancellationToken ct);
  Task<Result<CommunicationDto>> AppendCommunicationStatusChangeAsync(int communicationId, CommunicationStatusChangeDto statusChangeDto, CancellationToken ct);
}
