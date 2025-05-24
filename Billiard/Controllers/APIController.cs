using Billiard.Services.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly WorldNewsService _newsService;
        private readonly PexelsVideoService _pexelsVideoService;
        public APIController(WorldNewsService newsService, PexelsVideoService pexelsVideoService)
        {
            _newsService = newsService;
            _pexelsVideoService = pexelsVideoService;
        }

        [HttpGet("billiardvideo")]
        public async Task<IActionResult> GetBilliardVideos([FromQuery] int perPage = 10)
        {
            var videos = await _pexelsVideoService.SearchBilliardVideosAsync(perPage);
            // Chỉ trả về metadata, link video, không tải về file
            return Ok(videos);
        }

        [HttpGet("billiardnews")]
        public async Task<IActionResult> GetBilliardNews([FromQuery] int number = 10)
        {
            var articles = await _newsService.SearchBilliardNewsAsync(number);
            return Ok(articles);
        }
    }
}
