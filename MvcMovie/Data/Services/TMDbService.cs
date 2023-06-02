using MvcMovie.Models;
using Newtonsoft.Json;

namespace MvcMovie.Data.Services;

public class TMDbService
{
    private readonly HttpClient _httpClient;
    private readonly string? _apiKey;

    public TMDbService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;     
        _apiKey = config["TMDbAPI:Key"];
    }

    public async Task<SearchResultsModel> SearchMoviesAsync(string query, int page)
    {
        var url = $"search/movie?api_key={_apiKey}&language=en-US&query={query}&page={page}&include_adult=false";
        return await GetApiResponseAsync<SearchResultsModel>(url);
    }

    public async Task<SearchResultsModel> DiscoverMoviesByGenreAsync(GenreEnum genre, int page)
    {
        var url = $"discover/movie?api_key={_apiKey}&with_genres={(int)genre}&language=en-US&sort_by=vote_average.desc&vote_count.gte=10&include_adult=false&page={page}";
        return await GetApiResponseAsync<SearchResultsModel>(url);
    }

    private async Task<T> GetApiResponseAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get API response for URL: {url}");
        }
        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<T>(content);

        return apiResponse;
    }
}
