using NewsAPI.Models;
using NewsApp.Factory.Interface;
using NewsApp.ViewModel.ModelViewModel;

namespace NewsApp.Factory
{
    public class ArticleViewModelFactory : IArticleViewModelFactory
    {
        public ArticleViewModel CreateArticleViewModel(Article article)
        {
            ArticleViewModel viewModel = new()
            {
                Title = article.Title,
                Description = article.Description,
                Author = article.Author,
                PublishedAt = (System.DateTime)article.PublishedAt,
                SourceUrl = article.Url,
                ImageSource = article.UrlToImage != null ?
                              article.UrlToImage :
                              $"D:\\Projects\\Real projects C#\\NewsApp\\NewsApiProject\\NewsApp\\Resources\\Images\\NewsImage.png"
            };

            return viewModel;
        }
    }
}
