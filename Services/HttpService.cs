using System.Text.Json;
using Exporter.Models.Dtos;

namespace Exporter.Services;

public class HttpService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient();
    }

    public async Task<List<CardDto>> GetCardsAsync(string branchName = "")
    {
        var response = await _httpClient.GetAsync(Constants.GetGitHubCardsRepoUrl(branchName));

        if (!response.IsSuccessStatusCode)
            throw new Exception("An Http error occurred.");

        return JsonSerializer.Deserialize<List<CardDto>>(await response.Content.ReadAsStringAsync())!;
    }

    public async Task<List<SetDto>> GetSetsAsync(string branchName = "")
    {
        var response = await _httpClient.GetAsync(Constants.GetGitHubSetsRepoUrl(branchName));

        if (!response.IsSuccessStatusCode)
            throw new Exception("An Http error occurred.");

        return JsonSerializer.Deserialize<List<SetDto>>(await response.Content.ReadAsStringAsync())!;
    }
}