using FindrDataPointTest;

namespace ConsoleApp1;

public class GithubSkillGatherer
{
    private readonly IGithubHttpClient _client;

    public GithubSkillGatherer(IGithubHttpClient client)
    {
        _client = client;
    }

    public async Task<List<Skill>> GetSkillsFromUser(string username)
    {
        var repos = await _client.GetReposForUser(username);
        return new List<Skill>() { new(repos.First().Language) };
    }
}

public record Repository(string Language);