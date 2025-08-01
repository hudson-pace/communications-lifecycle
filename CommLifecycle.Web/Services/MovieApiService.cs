using SharedModels.DTOs;
namespace CommLifecycle.Web.Services;

public class MovieApiService
{
  private readonly HttpClient _http;
  public MovieApiService(IHttpClientFactory factory)
  {
    _http = factory.CreateClient("MoviesApi");
  }
  public async Task<List<CommunicationDto>?> GetCommunicationsAsync()
  {
    return await _http.GetFromJsonAsync<List<CommunicationDto>>("communications");
  }
  public async Task<CommunicationDto?> GetCommunicationAsync(int id)
  {
    return await _http.GetFromJsonAsync<CommunicationDto>($"communications/{id}");
  }
  public async Task CreateCommunicationAsync(CommunicationDto communicationDto)
  {
    await _http.PostAsJsonAsync("communications", communicationDto);
  }

  public async Task<List<CommunicationTypeDto>?> GetCommunicationTypesAsync()
  {
    return await _http.GetFromJsonAsync<List<CommunicationTypeDto>>("CommunicationTypes");
  }
  public async Task<CommunicationTypeDto?> GetCommunicationTypeAsync(int id)
  {
    return await _http.GetFromJsonAsync<CommunicationTypeDto>($"CommunicationTypes/{id}");
  }
  public async Task CreateCommunicationTypeAsync(CommunicationTypeDto communicationTypeDto)
  {
    await _http.PostAsJsonAsync("/CommunicationTypes", communicationTypeDto);
  }
  public async Task UpdateCommunicationTypeAsync(int communicationTypeId, CommunicationTypeDto communicationTypeDto)
  {
    await _http.PutAsJsonAsync($"/CommunicationTypes/{communicationTypeId}", communicationTypeDto);
  }

  public async Task CreateCommunicationStatusChange(int communicationId, CommunicationStatusChangeDto communicationStatusChangeDto)
  {
    await _http.PostAsJsonAsync($"/communications/{communicationId}/StatusHistory", communicationStatusChangeDto);
  }
}