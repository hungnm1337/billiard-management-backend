using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Billiard.Services.API
{
    public class PexelsVideoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public PexelsVideoService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["VideoApiKey"];
        }

        public async Task<List<PexelsVideo>> SearchBilliardVideosAsync(int perPage = 10)
        {
            var url = $"https://api.pexels.com/videos/search?query=billiard&per_page={perPage}&size=large&orientation=landscape";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", _apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PexelsVideoResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.Videos ?? new List<PexelsVideo>();
        }

    }

    public class PexelsVideoResponse
    {
        public List<PexelsVideo> Videos { get; set; }
    }

    public class PexelsVideo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public int Duration { get; set; }
        public List<PexelsVideoFile> Video_Files { get; set; }
    }

    public class PexelsVideoFile
    {
        public string Link { get; set; }
        public string Quality { get; set; }
        public string File_Type { get; set; }
    }
}
