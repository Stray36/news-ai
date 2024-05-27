using Autofac;
using NewsApp.Infrastructure;
using NewsApp.View;
using System.Windows;

namespace NewsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppServiceProvider.Initialize();

            var mainWindow = AppServiceProvider.Container.Resolve<NewsWindow>();
            mainWindow.Show();
        }
    }
}
