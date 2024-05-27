using Autofac;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsApp.Command;
using NewsApp.Factory;
using NewsApp.Infrastructure;
using NewsApp.Model;
using NewsApp.Service;
using NewsApp.View;
using NewsApp.ViewModel.ModelViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace NewsApp.ViewModel
{
    public class NewsViewModel : BaseViewModel
    {
        private readonly NewsService _newsService;
        private readonly SearchLineManagerService _searchLineManagerService;
        private readonly ArticleViewModelFactory _articleViewModelFactory;
        private readonly LocalizationService _localizationService;

        private readonly ObservableCollection<ArticleViewModel> _articles;
        public ObservableCollection<ArticleViewModel> Articles
        {
            get { return _articles; }
        }

        private string searchLine;
        public string SearchLine
        {
            get { return searchLine; }
            set
            {
                searchLine = value;
                OnPropertyChanged();
            }
        }

        private string callbackLine;
        public string CallbackLine
        {
            get { return callbackLine; }
            set
            {
                callbackLine = value;
                OnPropertyChanged();
            }
        }

        private readonly ObservableCollection<LocalizedEnum<Countries>> _countries;
        public ObservableCollection<LocalizedEnum<Countries>> Countries
        {
            get { return _countries; }
        }
        private LocalizedEnum<Countries> selectedCountry;
        public LocalizedEnum<Countries> SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                selectedCountry = value;
                OnPropertyChanged();
                ShowTopHeadlines();
            }
        }

        private readonly ObservableCollection<LocalizedEnum<Categories>> _categories;
        public ObservableCollection<LocalizedEnum<Categories>> Categories
        {
            get { return _categories; }
        }

        private readonly ObservableCollection<LocalizedEnum<SortBys>> _sortFilters;
        public ObservableCollection<LocalizedEnum<SortBys>> SortFilters
        {
            get { return _sortFilters; }
        }
        private LocalizedEnum<SortBys> selectedSortFilter;
        public LocalizedEnum<SortBys> SelectedSortFilter
        {
            get { return selectedSortFilter; }
            set
            {
                selectedSortFilter = value;
                OnPropertyChanged();
            }
        }

        private readonly ObservableCollection<LocalizedEnum<Localizations>> _localizations;
        public ObservableCollection<LocalizedEnum<Localizations>> Localizations
        {
            get { return _localizations; }
        }
        private LocalizedEnum<Localizations> selectedLocalization;
        public LocalizedEnum<Localizations> SelectedLocalization
        {
            get { return selectedLocalization; }
            set
            {
                selectedLocalization = value;
                OnPropertyChanged();
                if (value != null)
                {
                    _localizationService.ChangeLocalization(value.Value.ToString());
                    LoadElements();
                }
            }
        }



        private bool isTimeSpanEnabled;
        public bool IsTimeSpanEnabled
        {
            get { return isTimeSpanEnabled; }
            set
            {
                isTimeSpanEnabled = value;
                OnPropertyChanged();
            }
        }

        private DateTime fromDateTime;
        public DateTime FromDateTime
        {
            get { return fromDateTime; }
            set
            {
                fromDateTime = value;
                OnPropertyChanged();
            }
        }

        private DateTime toDateTime;
        public DateTime ToDateTime
        {
            get { return toDateTime; }
            set
            {
                toDateTime = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowTopHeadlinesCommand { get; private set; }
        public ICommand ShowTopHeadlinesByCategoryCommand { get; private set; }
        public ICommand OpenArticleInBrowserCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand OpenUrlCommand { get; private set; }

        #region Localization props
        private string newsTextBlock;
        public string News_TextBlock
        {
            get { return newsTextBlock; }
            set
            {
                newsTextBlock = value;
                OnPropertyChanged();
            }
        }
        private string countryLabel;
        public string Country_Label
        {
            get { return countryLabel; }
            set
            {
                countryLabel = value;
                OnPropertyChanged();
            }
        }
        private string searchLabel;
        public string Search_Label
        {
            get { return searchLabel; }
            set
            {
                searchLabel = value;
                OnPropertyChanged();
            }
        }
        private string localizationLabel;
        public string Localization_Label
        {
            get { return localizationLabel; }
            set
            {
                localizationLabel = value;
                OnPropertyChanged();
            }
        }
        private string searchButton;
        public string Search_Button
        {
            get { return searchButton; }
            set
            {
                searchButton = value;
                OnPropertyChanged();
            }
        }
        private string topHeadlinesTextBlock;
        public string TopHeadlines_TextBlock
        {
            get { return topHeadlinesTextBlock; }
            set
            {
                topHeadlinesTextBlock = value;
                OnPropertyChanged();
            }
        }
        private string filtersTextBlock;
        public string Filters_TextBlock
        {
            get { return filtersTextBlock; }
            set
            {
                filtersTextBlock = value;
                OnPropertyChanged();
            }
        }
        private string timeSpanCheckBox;
        public string TimeSpan_CheckBox
        {
            get { return timeSpanCheckBox; }
            set
            {
                timeSpanCheckBox = value;
                OnPropertyChanged();
            }
        }
        private string sortByLabel;
        public string SortBy_Label
        {
            get { return sortByLabel; }
            set
            {
                sortByLabel = value;
                OnPropertyChanged();
            }
        }
        private string fromLabel;
        public string From_Label
        {
            get { return fromLabel; }
            set
            {
                fromLabel = value;
                OnPropertyChanged();
            }
        }
        private string toLabel;
        public string To_Label
        {
            get { return toLabel; }
            set
            {
                toLabel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public NewsViewModel(NewsService newsService,
                             SearchLineManagerService searchLineManagerService,
                             ArticleViewModelFactory articleViewModelFactory,
                             LocalizationService localizationService)
        {
            _newsService = newsService;
            _searchLineManagerService = searchLineManagerService;
            _articleViewModelFactory = articleViewModelFactory;
            _localizationService = localizationService;

            _localizations = new ObservableCollection<LocalizedEnum<Localizations>>();
            _countries = new ObservableCollection<LocalizedEnum<Countries>>();
            _categories = new ObservableCollection<LocalizedEnum<Categories>>();
            _sortFilters = new ObservableCollection<LocalizedEnum<SortBys>>();
            _articles = new ObservableCollection<ArticleViewModel>();

            SelectedSortFilter = new LocalizedEnum<SortBys>() { Value = SortBys.Popularity, DisplayName = string.Empty };
            IsTimeSpanEnabled = false;
            CallbackLine = string.Empty;
            ToDateTime = FromDateTime = DateTime.Now - new TimeSpan(1, 0, 0, 0);

            ShowTopHeadlinesCommand = new RelayCommand(ShowTopHeadlines);
            SearchCommand = new RelayCommand(SearchArticles, CanSearchArticles);
            ShowTopHeadlinesByCategoryCommand = new RelayCommandWithParameter(ShowTopHeadlinesByCategory);
            OpenArticleInBrowserCommand = new RelayCommandWithParameter(OpenArticleInBrowser);

            _localizationService.ChangeLocalization(Model.Localizations.en.ToString());
            LoadElements();
            ShowTopHeadlines();
        }

        private void LoadElements()
        {
            News_TextBlock = _localizationService.GetLocalizationValue(nameof(News_TextBlock));
            Country_Label = _localizationService.GetLocalizationValue(nameof(Country_Label));
            Search_Label = _localizationService.GetLocalizationValue(nameof(Search_Label));
            Localization_Label = _localizationService.GetLocalizationValue(nameof(Localization_Label));
            Search_Button = _localizationService.GetLocalizationValue(nameof(Search_Button));
            TopHeadlines_TextBlock = _localizationService.GetLocalizationValue(nameof(TopHeadlines_TextBlock));
            Filters_TextBlock = _localizationService.GetLocalizationValue(nameof(Filters_TextBlock));
            TimeSpan_CheckBox = _localizationService.GetLocalizationValue(nameof(TimeSpan_CheckBox));
            SortBy_Label = _localizationService.GetLocalizationValue(nameof(SortBy_Label));
            From_Label = _localizationService.GetLocalizationValue(nameof(From_Label));
            To_Label = _localizationService.GetLocalizationValue(nameof(To_Label));

            Categories.Clear();
            foreach (var item in _localizationService.GetLocalizedEnumValues<Categories>())
            {
                Categories.Add(item);
            }

            Countries.Clear();
            foreach (var item in _localizationService.GetLocalizedEnumValues<Countries>())
            {
                Countries.Add(item);
            }

            SelectedCountry = SelectedCountry != null ?
                              Countries.Where(x => x.Value == SelectedCountry.Value).FirstOrDefault() :
                              Countries.Where(x => x.Value == NewsAPI.Constants.Countries.US).FirstOrDefault();

            Localizations.Clear();
            foreach (var item in _localizationService.GetLocalizedEnumValues<Localizations>())
            {
                Localizations.Add(item);
            }
            selectedLocalization = SelectedLocalization != null ?
                                   Localizations.Where(x => x.Value == SelectedLocalization.Value).FirstOrDefault() :
                                   Localizations.Where(x => x.Value == Model.Localizations.en).FirstOrDefault();

            Categories.Clear();
            foreach (var item in _localizationService.GetLocalizedEnumValues<Categories>())
            {
                Categories.Add(item);
            }

            SortFilters.Clear();
            SortFilters.Add(new LocalizedEnum<SortBys>() { Value = SortBys.Popularity, DisplayName = string.Empty });
            foreach (var item in _localizationService.GetLocalizedEnumValues<SortBys>())
            {
                SortFilters.Add(item);
            }
            if (SelectedSortFilter.DisplayName != string.Empty)
            {
                SelectedSortFilter = SortFilters.Where(x => x.Value == SelectedSortFilter.Value).FirstOrDefault();
            }
        }


        private void UpdateArticles(List<Article> articles)
        {
            Articles.Clear();
            foreach (var item in articles.Take(30))
            {
                Articles.Add(_articleViewModelFactory.CreateArticleViewModel(item));
            }
        }

        private bool CanSearchArticles()
        {
            return !string.IsNullOrEmpty(SearchLine);
        }

        private async void SearchArticles()
        {
            var parsedSearchLine = _searchLineManagerService.ParseSearchQuery(SearchLine);
            var query = new EverythingRequest()
            {
                Q = parsedSearchLine["KeyWord"].Count > 0 ? parsedSearchLine["KeyWord"].FirstOrDefault()
                                                          : null,
                Domains = parsedSearchLine["Domain"]
            };

            if (SelectedSortFilter.DisplayName != "")
            {
                query.SortBy = SelectedSortFilter.Value;
            }

            if (IsTimeSpanEnabled)
            {
                if (FromDateTime < ToDateTime)
                {
                    CallbackLine = "";
                    query.From = FromDateTime;
                    query.To = ToDateTime;
                }
                else
                {
                    CallbackLine = _localizationService.GetLocalizationValue("IncorrectTimeSpan");
                    return;
                }
            }

            var result = await _newsService.GetArticlesByRequestAsync(query);
            UpdateArticles(result);
        }

        private void OpenArticleInBrowser(object obj)
        {
            ArticleViewModel articleViewModel = (ArticleViewModel)obj;
            var viewModel = new ArticleWebViewModel(articleViewModel.SourceUrl);
            var webWindow = AppServiceProvider.Container.Resolve<ArticleWebWindow>(new TypedParameter(typeof(ArticleWebViewModel), viewModel));
            webWindow.Show();
        }

        private void OpenUrl(string parameter)
        {
            if (parameter is string url && !string.IsNullOrEmpty(url))
            {
                var viewModel = new ArticleWebViewModel(parameter);
                var webWindow = AppServiceProvider.Container.Resolve<ArticleWebWindow>(new TypedParameter(typeof(ArticleWebViewModel), viewModel));
                webWindow.Show();
            }
        }

        private void ShowTopHeadlinesByCategory(object obj)
        {
            LocalizedEnum<Categories> localizedCategory = (LocalizedEnum<Categories>)obj;
            var querry = new TopHeadlinesRequest()
            {
                Country = SelectedCountry.Value,
                Category = localizedCategory.Value
            };
            MakeTopHeadlinesRequesAsync(querry);
        }
        private void ShowTopHeadlines()
        {
            if (SelectedCountry != null)
            {
                var querry = new TopHeadlinesRequest()
                {
                    Country = SelectedCountry.Value
                };
                MakeTopHeadlinesRequesAsync(querry);
            }
        }
        private async void MakeTopHeadlinesRequesAsync(TopHeadlinesRequest topHeadlinesRequest)
        {
            var result = await _newsService.GetArticlesByRequestAsync(topHeadlinesRequest);
            if (result != null && result.Count > 0)
            {
                UpdateArticles(result);
            }
        }

    }
}
