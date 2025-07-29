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
  public async Task<List<Communication>> GetAllCommunicationsAsync()
  {
    List<Communication> communications = await _context.Communications.ToListAsync();
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
        StatusHistory = c.StatusHistory.Select(s => new CommunicationStatusChangeDto
        {
          Id = s.Id,
          CreatedAt = s.CreatedAt,
          Status = new CommunicationStatusDto
          {
            Id = s.Status.Id,
            Description = s.Status.Description,
          }
        }).ToList()
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
}
