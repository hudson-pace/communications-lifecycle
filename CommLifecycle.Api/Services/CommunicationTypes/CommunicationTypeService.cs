using Microsoft.EntityFrameworkCore;
using SharedModels.DTOs;
using SharedModels.Mappers;
using SharedModels.Models;
namespace CommLifecycle.Api.Services.CommunicationTypes;

public class CommunicationTypeService(CommLifecycleApiContext context, ILogger<CommunicationTypeService> logger) : ICommunicationTypeService
{
  private readonly CommLifecycleApiContext _context = context;
  private readonly ILogger<CommunicationTypeService> _logger = logger;

  public async Task<Result<List<CommunicationTypeDto>>> GetAllAsync(CancellationToken ct)
  {
    List<CommunicationTypeDto>? communicationTypes = await _context.CommunicationTypes
      .Select(c => new CommunicationTypeDto
      {
        Id = c.Id,
        Name = c.Name,
        Statuses = c.Statuses.Select(s => new CommunicationStatusDto
        {
          Id = s.Id,
          Description = s.Description,
        }).ToList()
      })
      .ToListAsync();
    return communicationTypes;
  }

  public async Task<Result<CommunicationTypeDto?>> GetByIdAsync(int id, CancellationToken ct)
  {
    CommunicationTypeDto? communicationType = await _context.CommunicationTypes
      .Where(c => c.Id == id)
      .Select(c => new CommunicationTypeDto
      {
        Id = c.Id,
        Name = c.Name,
        Statuses = c.Statuses.Select(s => new CommunicationStatusDto
        {
          Id = s.Id,
          Description = s.Description,
        }).ToList()
      })
      .FirstOrDefaultAsync();
    return communicationType;
  }
  public async Task<Result<CommunicationTypeDto>> CreateAsync(CommunicationTypeDto communicationTypeDto, CancellationToken ct)
  {
    if (communicationTypeDto is null) return Result<CommunicationTypeDto>.Failure(new ArgumentNullException(nameof(communicationTypeDto)));
    CommunicationType communicationType = communicationTypeDto.ToEntity();
    if (communicationType.Validate() is { IsFailure: true, Exception: Exception ex }) return Result<CommunicationTypeDto>.Failure(ex);
    _context.CommunicationTypes.Add(communicationType);
    Result result = await _context.TrySaveAsync(ct);
    return result.From(communicationType.ToDto());
  }
  public async Task<Result<CommunicationTypeDto>> UpdateAsync(int id, CommunicationTypeDto communicationTypeDto, CancellationToken ct)
  {
    CommunicationType? existingCommunicationType = await _context.CommunicationTypes.SingleOrDefaultAsync(c => c.Id == id, ct);
    if (existingCommunicationType is null) return Result<CommunicationTypeDto>.Failure(new EntityNotFoundException(nameof(CommunicationType), id));
    CommunicationType communicationType = communicationTypeDto.ToEntity();
    _context.CommunicationTypes.Update(communicationType);
    Result result = await _context.TrySaveAsync(ct);
    return result.From(communicationType.ToDto());
  }
  public async Task<Result<CommunicationTypeDto>> DeleteAsync(int id, CancellationToken ct)
  {
    CommunicationType? communicationType = await _context.CommunicationTypes.SingleOrDefaultAsync(c => c.Id == id, ct);
    if (communicationType is null) return Result<CommunicationTypeDto>.Failure(new EntityNotFoundException(nameof(CommunicationType), id));
    _context.Remove(communicationType);
    Result result = await _context.TrySaveAsync(ct);
    return result.From(communicationType.ToDto());
  }
}