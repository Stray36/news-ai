using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public NewsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchResults(string query)
        {
            var requestUrl = $"https://search.lepton.run/api/query?q={query}";
            var response = await _httpClient.GetFromJsonAsync<LeptonResponse>(requestUrl);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Results);
        }
    }

    public class LeptonResponse
    {
        public List<string> Results { get; set; }
    }
}
