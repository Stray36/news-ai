using NewsAPI.Models;
using NewsApp.Factory.Interface;
using NewsApp.ViewModel.ModelViewModel;

namespace NewsApp.Factory
{
    public class ArticleViewModelFactory : IArticleViewModelFactory
    {
        // 工厂类，创建一个ArticleViewModel实例
        public ArticleViewModel CreateArticleViewModel(Article article)
        {
            ArticleViewModel viewModel = new()
            {
                Title = article.Title, // 标题
                Description = article.Description, // 摘要
                Author = article.Author,  // 作者
                PublishedAt = (System.DateTime)article.PublishedAt, // 发布时间
                SourceUrl = article.Url, 
                ImageSource = article.UrlToImage != null ?
                              article.UrlToImage :
                              $"D:\\test\\News-ai\\NewsApp\\Resources\\ImagesNewsImage.png"
            };

            return viewModel;
        }
    }
}
