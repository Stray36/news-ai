using NewsApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApi.Services
{
    public class NewsService
    {
        private List<NewsItem> _newsItems;

        public NewsService()
        {
            // 初始化
            _newsItems = new List<NewsItem>
            {
                new NewsItem { Id = 1, Title = "News 1", Summary = "Summary 1" },
                new NewsItem { Id = 2, Title = "News 2", Summary = "Summary 2" },
                new NewsItem { Id = 3, Title = "News 3", Summary = "Summary 3" }
            };
        }

        public Task<List<NewsItem>> GetNewsAsync()
        {
            return Task.FromResult(_newsItems);
        }

        public Task<NewsItem> GetNewsByIdAsync(int id)
        {
            var newsItem = _newsItems.FirstOrDefault(n => n.Id == id);
            return Task.FromResult(newsItem);
        }

        public Task<List<NewsItem>> SearchNewsAsync(string searchTerm)
        {
            var results = _newsItems.Where(n => n.Title.Contains(searchTerm) || n.Summary.Contains(searchTerm)).ToList();
            return Task.FromResult(results);
        }

        public Task<List<NewsItem>> GetFavoriteNewsAsync()
        {
            // Placeholder for favorite news logic
            return Task.FromResult(_newsItems.Take(2).ToList());
        }

        public Task<List<NewsItem>> GetPushedNewsAsync()
        {
            // Placeholder for pushed news logic
            return Task.FromResult(_newsItems.Skip(1).Take(2).ToList());
        }
    }
}
