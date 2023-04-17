using System;
using ConsoleApp1;

namespace FindrDataPointTest.HttpClients
{
    public interface IStackoverflowHttpClient
    {
        public Task<List<Tag>> GetTagsForUser(string userId);
    }
}

