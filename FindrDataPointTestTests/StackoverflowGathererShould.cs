using FindrDataPointTest.HttpClients;
using FindrDataPointTest.SkillGatherers;
using FindrDataPointTest.SkillGatherers.Stackoverflow;
using NSubstitute;

namespace FindrDataPointTestTests
{
	public class StackoverflowGathererShould
	{
        [Test]
        public async Task ReturnSkillWhenUserHasOneTag()
        {
            // Arrange
            const string language = "Java";
            const string userId = "userId";
            const int count = 99;

            var tag = new List<Tag> { new(language, count) };
            var client = Substitute.For<IStackoverflowHttpClient>();
            client.GetTagsForUser(userId).Returns(tag);

            var gatherer = new StackoverflowSkillGatherer(client);
            var expected = new List<Skill> { new(language) };

            // Act
            var result = await gatherer.GetSkillsFromUserByTags(userId);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}

