using System;

namespace NewsApp.ViewModel.ModelViewModel
{
    public class ArticleViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string SourceUrl { get; set; }
        public string ImageSource { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
