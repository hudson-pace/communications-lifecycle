using System;
using SharedModels.Models;

using Microsoft.EntityFrameworkCore;

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
  public async Task<Communication?> GetCommunicationAsync(int id)
  {
    Communication? communication = await _context.Communications.FirstOrDefaultAsync(communication => communication.Id == id);
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
