using System;
using SharedModels.Models;

using Microsoft.EntityFrameworkCore;
using SharedModels.DTOs;
using System.Diagnostics;
using SharedModels.Mappers;

namespace CommLifecycle.Api.Services;

public class CommunicationService(CommLifecycleApiContext context, ILogger<CommunicationService> logger) : ICommunicationService
{
  private readonly CommLifecycleApiContext _context = context;
  private readonly ILogger<CommunicationService> _logger = logger;

  public async Task<Result<List<CommunicationDto>>> GetAllAsync(CancellationToken ct)
  {
    List<CommunicationDto>? communications = await _context.Communications
      .Select(c => c.ToDto())
      .ToListAsync(ct);
    return Result<List<CommunicationDto>>.Success(communications);
  }
  public async Task<Result<CommunicationDto>> GetByIdAsync(int id, CancellationToken ct)
  {
    CommunicationDto? communication = await _context.Communications
      .Where(c => c.Id == id)
      .Select(c => c.ToDto())
      .SingleOrDefaultAsync(ct);
    if (communication is null) return Result<CommunicationDto>.Failure(new EntityNotFoundException(nameof(CommunicationDto), id));
    return Result<CommunicationDto>.Success(communication);
  }
  public async Task<Result<CommunicationDto>> CreateAsync(CommunicationDto communicationDto, CancellationToken ct)
  {
    if (communicationDto is null) return Result<CommunicationDto>.Failure(new ArgumentNullException(nameof(communicationDto)));
    Communication communication = communicationDto.ToEntity();
    if (communication.Validate() is { IsFailure: true, Exception: Exception ex }) return Result<CommunicationDto>.Failure(ex);
    _context.Communications.Add(communication);
    Result result = await _context.TrySaveAsync(ct);
    return result.From(communication.ToDto());
  }
  public async Task<Result<CommunicationDto>> DeleteAsync(int id, CancellationToken ct)
  {
    Communication? communication = await _context.Communications
      .Where(c => c.Id == id)
      .SingleOrDefaultAsync(ct);
    if (communication is null) return Result<CommunicationDto>.Failure(new EntityNotFoundException(nameof(Communication), id));
    _context.Communications.Remove(communication);
    Result result = await _context.TrySaveAsync(ct);
    return result.From(communication.ToDto());
  }
  public async Task<Result<CommunicationDto>> UpdateAsync(int id, CommunicationDto communicationDto, CancellationToken ct)
  {
    if (communicationDto is null) return Result<CommunicationDto>.Failure(new ArgumentNullException(nameof(communicationDto)));
    Communication? communication = await _context.Communications.Where(c => c.Id == id).SingleOrDefaultAsync(ct);
    if (communication is null) return Result<CommunicationDto>.Failure(new EntityNotFoundException(nameof(CommunicationDto), id));

    communication.PatchFrom(communicationDto);
    if (communication.Validate() is { IsFailure: true, Exception: Exception ex }) return Result<CommunicationDto>.Failure(ex);
    _context.Communications.Update(communication);
    Result result = await _context.TrySaveAsync(ct);
    return result.From(communication.ToDto());
  }

  public async Task<Result<CommunicationDto>> AppendCommunicationStatusChangeAsync(
            int communicationId,
            CommunicationStatusChangeDto statusChangeDto,
            CancellationToken ct)
  {
    if (statusChangeDto is null) return Result<CommunicationDto>.Failure(new ArgumentNullException(nameof(statusChangeDto)));

    CommunicationStatusChange statusChange = statusChangeDto.ToEntity();
    if (statusChange.Validate() is { IsFailure: true, Exception: Exception ex }) return Result<CommunicationDto>.Failure(ex);

    Communication? communication = await _context.Communications.Where(c => c.Id == communicationId).FirstOrDefaultAsync();
    if (communication is null) return Result<CommunicationDto>.Failure(new EntityNotFoundException(nameof(Communication), communicationId));

    communication.StatusHistory.Add(statusChange);
    _context.Communications.Update(communication);
    Result result = await _context.TrySaveAsync(ct);
    return result.From(communication.ToDto());
  }
}


