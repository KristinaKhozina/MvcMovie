using Newtonsoft.Json;

namespace MvcMovie.Models;

public class SearchResultsModel
{
    public int Page { get; set; }
    [JsonProperty("results")]
    public List<MovieModel>? Movies { get; set; }
    [JsonProperty("total_results")]
    public int TotalResults { get; set; }
    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }
}