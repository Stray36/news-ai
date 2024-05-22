using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApi.Data;
using NewsApi.Models;
using NewsApi.Data;
using NewsApi.Models;
using System;
using System.Threading.Tasks;

// 新闻收藏控制器
namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly NewsContext _context;

        public FavoritesController(NewsContext context)
        {
            _context = context;
        }

        // 收藏新闻
        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteNews favoriteNews)
        {
            if (favoriteNews == null || string.IsNullOrEmpty(favoriteNews.Title) || string.IsNullOrEmpty(favoriteNews.Summary))
            {
                return BadRequest("不能为空.");
            }

            favoriteNews.DateAdded = DateTime.UtcNow;

            _context.FavoriteNews.Add(favoriteNews);
            await _context.SaveChangesAsync();

            return Ok(favoriteNews);
        }

        // 根据ID获取收藏的新闻
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsFavorite(int id)
        {
            var newsFavorite = await _context.FavoriteNews.FindAsync(id);

            if (newsFavorite == null)
            {
                return NotFound();
            }

            return Ok(newsFavorite);
        }

        // 获取所有新闻
        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var favorites = await _context.FavoriteNews.ToListAsync();
            return Ok(favorites);
        }
    }
}
