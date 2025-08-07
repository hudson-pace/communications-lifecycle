using CommLifecycle.Web.Services;

public class HttpService
{
    private readonly HttpClient _http;
    private readonly ToastService _toastService;

    public HttpService(HttpClient http, ToastService toastService)
    {
        _http = http;
        _toastService = toastService;
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        try
        {
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (HttpRequestException ex)
        {
            _toastService.ShowError($"Request failed: {ex.Message}");
            return default;
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Unexpected error: {ex.Message}");
            return default;
        }
    }
}