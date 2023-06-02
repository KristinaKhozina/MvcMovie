using MvcMovie.Data;
using Newtonsoft.Json;

namespace MvcMovie.Models;

public class MovieModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Overview { get; set; }

    [JsonProperty("genre_ids")]
    public GenreEnum[]? Genres { get; set; }
    [JsonProperty("poster_path")]
    public string? PosterPath { get; set; }
    [JsonProperty("release_date")]
    public string? ReleaseDate { get; set; }
    [JsonProperty("vote_average")]
    public double Rating { get; set; }
}
