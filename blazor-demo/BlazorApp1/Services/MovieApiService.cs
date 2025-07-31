using BlazorApp1.Models;
using SharedModels.DTOs;
namespace BlazorApp1.Services;

public class MovieApiService
{
  private readonly HttpClient _http;
  public MovieApiService(IHttpClientFactory factory)
  {
    _http = factory.CreateClient("MoviesApi");
  }
  public async Task<List<Movie>?> GetMoviesAsync()
  {
    return await _http.GetFromJsonAsync<List<Movie>>("movies");
  }
  public async Task<Movie?> CreateMovieAsync(Movie movie)
  {
    var response = await _http.PostAsJsonAsync("movies", movie);
    return await response.Content.ReadFromJsonAsync<Movie>();
  }

  public async Task<List<CommunicationDto>?> GetCommunicationsAsync()
  {
    return await _http.GetFromJsonAsync<List<CommunicationDto>>("communications");
  }

  public async Task<List<CommunicationTypeDto>?> GetCommunicationTypesAsync()
  {
    return await _http.GetFromJsonAsync<List<CommunicationTypeDto>>("CommunicationTypes");
  }
}