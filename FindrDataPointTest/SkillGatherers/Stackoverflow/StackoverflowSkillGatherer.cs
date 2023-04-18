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

        public async Task<List<Skill>> GetSkillsFromUserByTags(string userId)
        {
            var tags = await client.GetTagsForUser(userId);
            return tags.Select(tag => new Skill(tag.name)).ToList();
        }

        public async Task<List<Skill>> GetSkillsFromUserByAnswers(string userId)
        {
            var answers = await client.GetAnswersForUser(userId);
            var question_ids = answers.Select(answer => answer.question_id).ToList();
            var questions = await client.GetQuestionsForUser(question_ids);
            var skills = questions.Select(question => question.tags)
                .SelectMany(tags => tags)
                .Distinct()
                .Select(tag => new Skill(tag));
            return skills.ToList();
        }
    }
}

