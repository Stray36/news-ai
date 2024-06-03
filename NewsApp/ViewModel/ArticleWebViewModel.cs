using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.ComponentModel;
using System;

namespace NewsApp.ViewModel
{
    public class ArticleWebViewModel : BaseViewModel
    {
        private string articleUrl;
        private string articleSummary;

        public string ArticleUrl
        {
            get { return articleUrl; }
            private set
            {
                articleUrl = value;
                OnPropertyChanged();
                _ = FetchArticleSummaryAsync();
            }
        }

        public string ArticleSummary
        {
            get { return articleSummary; }
            set
            {
                articleSummary = value; 
                OnPropertyChanged();
            }
        }

        private async Task FetchArticleSummaryAsync()
        {
            if (string.IsNullOrWhiteSpace(ArticleUrl))
                return;

            using HttpClient client = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(new { url = ArticleUrl }), Encoding.UTF8, "application/json");


            try
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost:8000/summarize", content);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                // var result = JsonSerializer.Deserialize<SummaryResponse>(jsonResponse);

                // ArticleSummary = result?.Summary;
                ArticleSummary = jsonResponse?.ToString();
                // ArticleSummary = response.ToString();
            }
            catch (Exception ex)
            {
                ArticleSummary = $"Error: {ex.Message}";
            }
        }

        public ArticleWebViewModel(string articleUrl)
        {
            ArticleUrl = articleUrl;
        }

        private class SummaryResponse
        {
            public string Summary { get; set; }
        }
    }
}
