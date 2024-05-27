using NewsAPI.Models;
using NewsApp.ViewModel.ModelViewModel;

namespace NewsApp.Factory.Interface
{
    public interface IArticleViewModelFactory
    {
        ArticleViewModel CreateArticleViewModel(Article article);
    }
}
