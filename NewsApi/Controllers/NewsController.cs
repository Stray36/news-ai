using Microsoft.AspNetCore.Mvc;
using NewsApi.Models;
using NewsApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NewsItem>>> GetNews()
        {
            var newsItems = await _newsService.GetNewsAsync();
            return Ok(newsItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsItem>> GetNewsById(int id)
        {
            var newsItem = await _newsService.GetNewsByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }
            return Ok(newsItem);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<NewsItem>>> SearchNews([FromQuery] string query)
        {
            var newsItems = await _newsService.SearchNewsAsync(query);
            return Ok(newsItems);
        }

        [HttpGet("favorites")]
        public async Task<ActionResult<List<NewsItem>>> GetFavoriteNews()
        {
            var newsItems = await _newsService.GetFavoriteNewsAsync();
            return Ok(newsItems);
        }

        [HttpGet("pushed")]
        public async Task<ActionResult<List<NewsItem>>> GetPushedNews()
        {
            var newsItems = await _newsService.GetPushedNewsAsync();
            return Ok(newsItems);
        }
    }
}
