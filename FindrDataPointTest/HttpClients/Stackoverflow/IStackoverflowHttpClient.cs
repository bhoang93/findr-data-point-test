using FindrDataPointTest.HttpClients.Stackoverflow;

namespace FindrDataPointTest.HttpClients
{
    public interface IStackoverflowHttpClient
    {
        public Task<List<Tag>> GetTagsForUser(string userId);
        public Task<List<Answer>> GetAnswersForUser(string userId);
        public Task<List<Question>> GetQuestionsForUser(List<string> questionIds);
    }
}

