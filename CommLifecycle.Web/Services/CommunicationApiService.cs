using SharedModels.DTOs;
namespace CommLifecycle.Web.Services;

public class CommunicationApiService
{
  private readonly HttpClient _http;
  private readonly ToastService _toastService;
  public CommunicationApiService(IHttpClientFactory factory, ToastService toastService)
  {
    _http = factory.CreateClient("CommLifecycle.Api");
    _toastService = toastService;
  }

  private async Task<ApiResult<T>> TryRequestAsync<T>(Func<Task<HttpResponseMessage>> request)
  {
    try
    {
      var response = await request();
      if (response.IsSuccessStatusCode)
      {
        var data = await response.Content.ReadFromJsonAsync<T>();
        return ApiResult<T>.Ok(data!);
      }

      var error = await response.Content.ReadAsStringAsync();
      _toastService.ShowError($"Error {response.StatusCode}: {error}");
      return ApiResult<T>.Fail(error);
    }
    catch (Exception ex)
    {
      _toastService.ShowError($"Unexpected error: {ex.Message}");
      return ApiResult<T>.Fail(ex.Message);
    }

  }

  public async Task<List<CommunicationDto>?> GetCommunicationsAsync()
  {
    return await _http.GetFromJsonAsync<List<CommunicationDto>>("communications");
  }
  public async Task<CommunicationDto?> GetCommunicationAsync(int id)
  {
    return await _http.GetFromJsonAsync<CommunicationDto>($"communications/{id}");
  }
  /*
  public async Task<ApiResult<CommunicationDto?>> GetCommunicationAsync(int id)
  {
    return await TryRequestAsync<CommunicationDto>(() => _http.GetAsync($"communications/{id}"));
  }
  */
  public async Task CreateCommunicationAsync(CommunicationDto communicationDto)
  {
    await _http.PostAsJsonAsync("communications", communicationDto);
  }

  /*
  public async Task<ApiResult<bool>> CreateCommunicationAsync(CommunicationDto dto)
  {
    try
    {
      var response = await _http.PostAsJsonAsync("communications", dto);
      if (response.IsSuccessStatusCode)
        return ApiResult<bool>.Ok(true);

      var error = await response.Content.ReadAsStringAsync();
      _toastService.ShowError($"Failed to create: {error}");
      return ApiResult<bool>.Fail(error);
    }
    catch (Exception ex)
    {
      _toastService.ShowError($"Unexpected error: {ex.Message}");
      return ApiResult<bool>.Fail(ex.Message);
    }
  }
*/

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
  public async Task UpdateCommunicationTypeAsync(CommunicationTypeDto communicationType)
  {
    await _http.PutAsJsonAsync($"/CommunicationTypes/{communicationType.Id}", communicationType);
  }
}