using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using NewsAPI;
using NewsApp.Factory;
using NewsApp.Service;
using NewsApp.View;
using NewsApp.ViewModel;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace NewsApp.Infrastructure
{
    public static class AppServiceProvider
    {
        private static bool IsInitialized = false;
        public static IContainer Container { get; private set; }
        public static void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            var services = new ContainerBuilder();

            // ViewModel
            services.RegisterType<NewsViewModel>();
            services.RegisterType<ArticleWebViewModel>();

            // Window
            services.RegisterType<NewsWindow>();
            services.RegisterType<ArticleWebWindow>();

            // Factory
            services.RegisterType<ArticleViewModelFactory>();

            // Service
            services.RegisterType<NewsApiClient>().WithParameter("apiKey", GetStringFromAppSettings("ApiKeys", "NewsApi"));
            services.RegisterType<NewsService>();
            services.RegisterType<SearchLineManagerService>();
            services.RegisterType<ResourceManager>().WithParameters(new List<Parameter>()
            {
                new NamedParameter("baseName", GetStringFromAppSettings("Resources", "Localization")),
                new NamedParameter("assembly", Assembly.GetExecutingAssembly())
            });
            services.RegisterType<LocalizationService>();

            Container = services.Build();
            IsInitialized = true;
        }

        public static string GetStringFromAppSettings(string section, string key)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
            var value = configuration.GetSection(section);
            return value[key];
        }
    }
}