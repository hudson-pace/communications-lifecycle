using System;
using SharedModels.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTOs;

namespace MoviesApi.Services;

public interface ICommunicationService
{
  Task<List<Communication>> GetAllCommunicationsAsync();
  Task<CommunicationDto?> GetCommunicationAsync(int id);
  Task<Communication> CreateCommunicationAsync(Communication Communication);
  Task<Communication> DeleteCommunicationAsync(Communication Communication);
}
