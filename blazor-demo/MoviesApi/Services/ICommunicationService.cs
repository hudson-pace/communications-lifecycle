using System;
using SharedModels.Models;
using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Services;

public interface ICommunicationService
{
  Task<List<Communication>> GetAllCommunicationsAsync();
  Task<Communication?> GetCommunicationAsync(int id);
  Task<Communication> CreateCommunicationAsync(Communication Communication);
  Task<Communication> DeleteCommunicationAsync(Communication Communication);
}
