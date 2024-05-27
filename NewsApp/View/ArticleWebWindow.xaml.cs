using NewsApp.ViewModel;
using System.Windows;

namespace NewsApp.View
{
    /// <summary>
    /// Interaction logic for ArticleWebWindow.xaml
    /// </summary>
    public partial class ArticleWebWindow : Window
    {
        public ArticleWebWindow(ArticleWebViewModel articleWebViewModel)
        {
            InitializeComponent();
            DataContext = articleWebViewModel;
        }
    }
}
