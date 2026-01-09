using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Book2Screen.Models;
using Book2Screen.Services.Abstractions;

namespace Book2Screen.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMovieService _movieService;

    public HomeController(ILogger<HomeController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    public async Task<IActionResult> Index()
    {
        var upcomingMovies = await _movieService.GetUpcomingMoviesAsync();
        var upcomingMoviesList = upcomingMovies.OrderByDescending(u => u.Releasedate).Take(4);

        return View(upcomingMoviesList);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

