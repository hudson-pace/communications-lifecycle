using System;
using SharedModels.Models;

using Microsoft.EntityFrameworkCore;
using SharedModels.DTOs;

namespace MoviesApi.Services;

public class CommunicationService : ICommunicationService
{
  private readonly MoviesApiContext _context;
  public CommunicationService(MoviesApiContext context)
  {
    _context = context;
  }
  public async Task<List<CommunicationDto>> GetAllCommunicationsAsync()
  {
    List<CommunicationDto>? communications = await _context.Communications
      .Select(c => new CommunicationDto
      {
        Id = c.Id,
        Title = c.Title,
        Type = new CommunicationTypeDto
        {
          Id = c.Type.Id,
          Name = c.Type.Name,
        },
        StatusHistory = c.StatusHistory
        .OrderBy(s => s.CreatedAt)
        .Select(s => new CommunicationStatusChangeDto
        {
          Id = s.Id,
          CreatedAt = s.CreatedAt,
          Status = new CommunicationStatusDto
          {
            Id = s.Status.Id,
            Description = s.Status.Description,
          }
        })
        .ToList()
      })
      .ToListAsync();
    return communications;
  }
  public async Task<CommunicationDto?> GetCommunicationAsync(int id)
  {
    CommunicationDto? communication = await _context.Communications
      .Where(c => c.Id == id)
      .Select(c => new CommunicationDto
      {
        Id = c.Id,
        Title = c.Title,
        Type = new CommunicationTypeDto
        {
          Id = c.Type.Id,
          Name = c.Type.Name,
        },
        StatusHistory = c.StatusHistory
        .OrderBy(s => s.CreatedAt)
        .Select(s => new CommunicationStatusChangeDto
        {
          Id = s.Id,
          CreatedAt = s.CreatedAt,
          Status = new CommunicationStatusDto
          {
            Id = s.Status.Id,
            Description = s.Status.Description,
          }
        })
        .ToList()
      })
      .FirstOrDefaultAsync();
    return communication;
  }
  public async Task<Communication> CreateCommunicationAsync(Communication Communication)
  {
    _context.Communications.Add(Communication); ;
    await _context.SaveChangesAsync();
    return Communication;
  }
  public async Task<Communication> DeleteCommunicationAsync(Communication Communication)
  {
    _context.Communications.Remove(Communication);
    await _context.SaveChangesAsync();
    return Communication;
  }

  public async Task<List<CommunicationTypeDto>> GetAllCommunicationTypesAsync()
  {
    List<CommunicationTypeDto>? communicationType = await _context.CommunicationTypes
      .Select(c => new CommunicationTypeDto
      {
        Id = c.Id,
        Name = c.Name,
      })
      .ToListAsync();
    return communicationType;
  }
  public async Task<CommunicationTypeDto?> GetCommunicationTypeAsync(int id)
  {
    CommunicationTypeDto? communicationType = await _context.CommunicationTypes
      .Where(c => c.Id == id)
      .Select(c => new CommunicationTypeDto
      {
        Id = c.Id,
        Name = c.Name,
      })
      .FirstOrDefaultAsync();
    return communicationType;
  }
  public async Task CreateCommunicationTypeAsync(CommunicationTypeDto Communication)
  {
  }
}


