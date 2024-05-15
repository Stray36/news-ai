using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class NewsService
{
    private readonly HttpClient _httpClient;

    public NewsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<NewsItem[]?> GetNewsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<NewsItem[]>("https://api.example.com/news");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public async Task<NewsItem[]?> GetFavoriteNewsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<NewsItem[]>("https://api.example.com/favorites");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public async Task<NewsItem[]?> GetPushedNewsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<NewsItem[]>("https://api.example.com/pushed");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public async Task<string> GenerateSummaryAsync(string originalText)
    {
        var response = await _httpClient.PostAsJsonAsync("https://api.example.com/generate-summary", new { text = originalText });
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<SummaryResult>();
        return result?.Summary ?? "No summary available";
    }

    public async Task<NewsItem[]?> SearchNewsAsync(string searchTerm)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<NewsItem[]>($"https://api.example.com/news/search?query={searchTerm}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public async Task<NewsItem?> GetNewsDetailAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<NewsItem>($"https://api.example.com/news/{id}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public class NewsItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Summary { get; set; }
    }

    public class SummaryResult
    {
        public string? Summary { get; set; }
    }
}
