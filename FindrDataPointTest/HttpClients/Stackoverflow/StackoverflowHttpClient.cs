// See https://aka.ms/new-console-template for more information

using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace FindrDataPointTest.HttpClients.Stackoverflow;

internal class StackoverflowHttpClient : IStackoverflowHttpClient
{
    private readonly HttpClient _client = new();

    public StackoverflowHttpClient()
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        _client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<Tag>> GetTagsForUser(string userId)
    {
        string requestUri = $"https://api.stackexchange.com/2.3/users/{userId}/tags?order=desc&sort=popular&site=stackoverflow";
        HttpResponseMessage response = await _client.GetAsync(requestUri);

        // read response content as byte array
        string responseString = await UncompressResponse(response);

        await using Stream stream =
            await _client.GetStreamAsync("https://api.stackexchange.com/2.3/users/13570600/tags?order=desc&sort=popular&site=stackoverflow");

        var data = JsonConvert.DeserializeObject<TagResponse>(responseString);

        return data.items;
    }

    public async Task<List<Answer>> GetAnswersForUser(string userId)
    {
        string requestUri = $"https://api.stackexchange.com/2.3/users/{userId}/answers?order=desc&sort=activity&site=stackoverflow";
        HttpResponseMessage response = await _client.GetAsync(requestUri);

        // read response content as byte array
        string responseString = await UncompressResponse(response);

        await using Stream stream =
            await _client.GetStreamAsync("https://api.stackexchange.com/2.3/users/13570600/tags?order=desc&sort=popular&site=stackoverflow");

        var data = JsonConvert.DeserializeObject<AnswerResponse>(responseString);

        return data.items;
    }

    private static async Task<string> UncompressResponse(HttpResponseMessage response)
    {
        byte[] responseContent = await response.Content.ReadAsByteArrayAsync();

        // check if response is compressed using gzip
        if (response.Content.Headers.ContentEncoding.Contains("gzip"))
        {
            // decompress response using GZipStream
            using (var gzipStream = new GZipStream(new MemoryStream(responseContent), CompressionMode.Decompress))
            using (var decompressedStream = new MemoryStream())
            {
                await gzipStream.CopyToAsync(decompressedStream);
                responseContent = decompressedStream.ToArray();
            }
        }

        // convert response content to string
        return Encoding.UTF8.GetString(responseContent);
     
    }

    public async Task<List<Question>> GetQuestionsForUser(List<string> questionIds)
    {
        var question_Ids = String.Join(";", questionIds);
        string requestUri = $"https://api.stackexchange.com/2.3/questions/{question_Ids}?order=desc&sort=activity&site=stackoverflow";
        HttpResponseMessage response = await _client.GetAsync(requestUri);

        // read response content as byte array
        string responseString = await UncompressResponse(response);

        var data = JsonConvert.DeserializeObject<QuestionResponse>(responseString);

        return data.items;
    }
}