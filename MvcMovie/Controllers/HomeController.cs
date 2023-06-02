using Microsoft.AspNetCore.Mvc;
using MvcMovie.Data.Services;
using MvcMovie.Models;
using MvcMovie.Data;

namespace MvcMovie.Controllers;

public class HomeController : Controller
{
    private readonly TMDbService _movieApiService;
    public HomeController(TMDbService movieApiService)
    {
        _movieApiService = movieApiService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("Search/Title/{query?}")]
    public async Task<IActionResult> SearchByTitle(string query, int page = 1)
    {
        if (string.IsNullOrEmpty(query))
            return View("Index");

        var model = new SearchResultsViewModel()
        {
            Query = query,
            SearchResult = await _movieApiService.SearchMoviesAsync(query, page)
        };
        model.Parse();
        if (model.SearchResult.Movies.Count == 0) 
            return View("NotFound", model);
        return View("Search", model);
    }

    [Route("Search/Genre/{genre?}")]
    public async Task<IActionResult> SearchByGenre(GenreEnum genre, int page = 1)
    {
        var model = new SearchResultsViewModel()
        {
            Query = genre.ToString(),
            SearchResult = await _movieApiService.DiscoverMoviesByGenreAsync(genre, page)
        };
        model.Parse();
        return View("Search", model);
    }
}