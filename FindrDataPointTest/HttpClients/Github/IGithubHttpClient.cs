using ConsoleApp1;

namespace FindrDataPointTest.HttpClients;

public interface IGithubHttpClient
{
    public Task<List<Repository>> GetReposForUser(string username);
}