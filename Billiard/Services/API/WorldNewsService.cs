using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Billiard.Services.API
{
    public class WorldNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WorldNewsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["WorldNewsApiKey"];
        }

        public async Task<List<WorldNewsArticle>> SearchBilliardNewsAsync(int number = 10)
        {
            var url = $"https://api.worldnewsapi.com/search-news?text=billiard&language=en&number={number}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-Api-Key", _apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<WorldNewsResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.News ?? new List<WorldNewsArticle>();
        }
    }

    public class WorldNewsResponse
    {
        public List<WorldNewsArticle> News { get; set; }
    }
    public class WorldNewsArticle
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string PublishDate { get; set; }
    }
}
