using ConsoleApp1;
using FindrDataPointTest;
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
        
        var repos = new List<Repository>() {new(language)};
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