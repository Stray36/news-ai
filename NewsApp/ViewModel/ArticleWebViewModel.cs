namespace NewsApp.ViewModel
{
    public class ArticleWebViewModel : BaseViewModel
    {
        private string articleUrl;
        public string ArticleUrl
        {
            get { return articleUrl; }
            private set
            {
                articleUrl = value;
                OnPropertyChanged();
            }
        }
        public ArticleWebViewModel(string articleUrl)
        {
            ArticleUrl = articleUrl;
        }
    }
}
