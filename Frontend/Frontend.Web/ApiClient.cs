using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace Frontend.Web;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    public ApiClient(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    private async Task AddAuthorizationHeaderAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");
        if (!string.IsNullOrWhiteSpace(token))
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
    }
    public async Task<List<T>> GetCollectionAsync<T>(string path, int maxItems = 10, CancellationToken cancellationToken = default)
    {
        await AddAuthorizationHeaderAsync();
        return await _httpClient.GetFromJsonAsync<List<T>>(path, cancellationToken);
    }   
    
    public async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken = default)
    {
        await AddAuthorizationHeaderAsync();
        var response = await _httpClient.GetFromJsonAsync<T>(path, cancellationToken);
        if (response is null)
        {
            throw new NullReferenceException("No results");
        }

        return response;
    }

    public async Task PostAsync<T>(string path, T model, CancellationToken cancellationToken = default)
    {
        await AddAuthorizationHeaderAsync();
        await _httpClient.PostAsJsonAsync<T>(path, model);

    }

    public async Task PostWithFormAsync(string path, MultipartFormDataContent model, CancellationToken cancellationToken = default)
    {
        await AddAuthorizationHeaderAsync();
        await _httpClient.PostAsync(path, model, cancellationToken);
    }
    public async Task PutAsync<T>(string path, T model, CancellationToken cancellationToken = default)
    {
        await AddAuthorizationHeaderAsync();
        await _httpClient.PutAsJsonAsync<T>(path, model);
    }


    public async Task DeleteAsync(string path, CancellationToken cancellation = default)
    {
        await AddAuthorizationHeaderAsync();
        await _httpClient.DeleteAsync(path, cancellation);
    }
}

