using ConsoleApp1;
using FindrDataPointTest.HttpClients;
using FindrDataPointTest.SkillGatherers;
using NSubstitute;

namespace FindrDataPointTestTests;

public class GithubGathererShould
{
    [Test]
    public async Task ReturnSkillWhenUserHasOneRepo()
    {
        // Arrange
        const string language = "Java";
        const string username = "username";

        DateTime now = DateTime.Now;
        var repos = new List<Repository> {new(language, now, now, now)};
        var client = Substitute.For<IGithubHttpClient>();
        client.GetReposForUser(username).Returns(repos);
        
        var gatherer = new GithubSkillGatherer(client);
        var expected = new List<Skill> {new(language)};

        // Act
        var result = await gatherer.GetSkillsFromUser(username);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public async Task ReturnSkillWhenUserHasTwoRepos()
    {
        // Arrange
        const string language = "Java";
        const string language2 = "TypeScript";
        const string username = "username";

        DateTime now = DateTime.Now;
        var repos = new List<Repository>
        {
            new(language, now, now, now),
            new(language2, now, now, now)
        };
        var client = Substitute.For<IGithubHttpClient>();
        client.GetReposForUser(username).Returns(repos);
        
        var gatherer = new GithubSkillGatherer(client);
        var expected = new List<Skill> {new(language), new(language2)};

        // Act
        var result = await gatherer.GetSkillsFromUser(username);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test] public async Task ReturnOnlyUniqueSkillsWhenUserHasMultipleRepos()
    {
        // Arrange
        const string language = "Java";
        const string username = "username";

        DateTime now = DateTime.Now;
        var repos = new List<Repository>
        {
            new(language, now, now, now),
            new(language, now, now, now)
        };
        var client = Substitute.For<IGithubHttpClient>();
        client.GetReposForUser(username).Returns(repos);
        
        var gatherer = new GithubSkillGatherer(client);
        var expected = new List<Skill> {new(language)};

        // Act
        var result = await gatherer.GetSkillsFromUser(username);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}