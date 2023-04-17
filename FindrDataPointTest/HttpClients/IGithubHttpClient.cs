namespace ConsoleApp1;

public interface IGithubHttpClient
{
    public async Task<List<Repository>> GetReposForUser(string username)
    {
        throw new NotImplementedException();
    }
}