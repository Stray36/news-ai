using NewsApp.ViewModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace NewsApp.View
{
    /// <summary>
    /// Interaction logic for NewsWindow.xaml
    /// </summary>
    public partial class NewsWindow : Window
    {
        public NewsWindow(NewsViewModel newsViewModel)
        {
            InitializeComponent();
            DataContext = newsViewModel;
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }
}