namespace MvcMovie.Models;

public class SearchResultsViewModel
{
    public string? Query { get; set; }
    public SearchResultsModel? SearchResult { get; set; }

    public void Parse()
    {
        if (SearchResult == null) return;
        if (SearchResult.Movies == null) return;

        SearchResult.Movies.RemoveAll(x => string.IsNullOrEmpty(x.ReleaseDate) &&
        (string.IsNullOrEmpty(x.PosterPath) || !x.Genres.Any()));

        SearchResult.TotalPages = Math.Min(500, SearchResult.TotalPages);

        foreach (var movie in SearchResult.Movies)
        {
            if (!string.IsNullOrEmpty(movie.ReleaseDate))
            {
                movie.ReleaseDate = movie.ReleaseDate.Remove(4);
                movie.ReleaseDate = movie.ReleaseDate.Insert(0, "(") + ")";
            }
            else
                movie.ReleaseDate = " ";

            if (!string.IsNullOrEmpty(movie.PosterPath))
            {
                var TmdbBaseImageUrl = "https://image.tmdb.org/t/p/w500/";
                movie.PosterPath = TmdbBaseImageUrl + movie.PosterPath;
            }
            else
                movie.PosterPath = "/Images/no-poster.png";
        }
    }
}