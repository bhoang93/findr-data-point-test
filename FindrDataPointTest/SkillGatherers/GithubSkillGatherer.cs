using FindrDataPointTest.HttpClients;

namespace FindrDataPointTest.SkillGatherers;

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
        return repos.Select(repo => new Skill(repo.language)).Distinct().ToList();
    }
}