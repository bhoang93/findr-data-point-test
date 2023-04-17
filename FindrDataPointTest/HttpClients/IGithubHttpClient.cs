using System.Net.Http.Headers;
using System.Text.Json;
using ConsoleApp1;

namespace FindrDataPointTest.HttpClients;

public interface IGithubHttpClient
{
    public Task<List<Repository>> GetReposForUser(string username);
}

class GithubHttpClient : IGithubHttpClient
{
    private readonly HttpClient _client = new();
    
    public GithubHttpClient()
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
    }
    public async Task<List<Repository>> GetReposForUser(string username)
    {
        await using Stream stream =
            await _client.GetStreamAsync($"https://api.github.com/users/{username}/repos");
        
        var repositories =
            await JsonSerializer.DeserializeAsync<List<Repository>>(stream);

        return repositories;
    }
}