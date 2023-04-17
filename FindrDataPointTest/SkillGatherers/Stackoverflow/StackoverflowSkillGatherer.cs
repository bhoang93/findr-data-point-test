using System;
using FindrDataPointTest.HttpClients;

namespace FindrDataPointTest.SkillGatherers.Stackoverflow
{
    public class StackoverflowSkillGatherer
    {
        private IStackoverflowHttpClient client;

        public StackoverflowSkillGatherer(IStackoverflowHttpClient client)
        {
            this.client = client;
        }

        public async Task<List<Skill>> GetSkillsFromUser(string userId)
        {
            var tags = await client.GetTagsForUser(userId);
            return tags.Select(tag => new Skill(tag.name)).ToList();
        }
    }
}

