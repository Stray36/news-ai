using NewsAPI;
using NewsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Service
{
    public class NewsService
    {
        private readonly NewsApiClient _newsApiClient;
        public NewsService(NewsApiClient newsApiClient)
        {
            _newsApiClient = newsApiClient;
        }

        public async Task<List<Article>> GetArticlesByRequestAsync(TopHeadlinesRequest topHeadlinesRequest)
        {
            var articles = await _newsApiClient.GetTopHeadlinesAsync(topHeadlinesRequest);
            return articles.Articles.Where(x => x.Title != "[Removed]" &&
                                              x.Description != "[Removed]").ToList();
        }
        public async Task<List<Article>> GetArticlesByRequestAsync(EverythingRequest everythingRequest)
        {
            var articles = await _newsApiClient.GetEverythingAsync(everythingRequest);
            return articles.Articles.Where(x => x.Title != "[Removed]" &&
                                              x.Description != "[Removed]").ToList();
        }
    }
}