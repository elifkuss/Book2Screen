
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Book2Screen.Models;
using Book2Screen.Services.Abstractions;
using System.Security.Claims;
using Book2Screen.Services;
using Book2Screen.Services.Concretes;
namespace Book2Screen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICommentService _commentService;

        public MovieController(IMovieService movieService , ICommentService commentService)
        {
            _movieService = movieService;
            _commentService = commentService;
        }


        // GET: api/Movie
        [HttpGet]
       
        public async Task<IActionResult> Index(string searchTerm = null)
        {
            IEnumerable<Movie> movies;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                movies = await _movieService.GetAllMoviesAsync();
            }
            else
            {
                movies = await _movieService.SearchMoviesAsync(searchTerm);
            }

            ViewData["SearchTerm"] = searchTerm;
            return View(movies);
        }



        // GET: api/Movie/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync((int)id);
            if (movie == null)
            {
                return NotFound();
            }

            var comments = await _movieService.GetCommentsForMovieAsync((int)id);
            ViewData["Comments"] = comments;

            return View(movie);
        }

        // GET: api/Movie/Create
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["BookId"] = new SelectList(await _movieService.GetBooksAsync(), "BookId", "BookId");
            ViewData["DirectorId"] = new SelectList(await _movieService.GetDirectorsAsync(), "DirectorId", "DirectorId");
            return View();
        }

        // POST: api/Movie/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,Score,Movietype,Releasedate,Movieduration,Budget,Boxofficerevenue,BookId,DirectorId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.AddAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(await _movieService.GetBooksAsync(), "BookId", "BookId", movie.BookId);
            ViewData["DirectorId"] = new SelectList(await _movieService.GetDirectorsAsync(), "DirectorId", "DirectorId", movie.DirectorId);
            return View(movie);
        }

        // GET: api/Movie/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync((int)id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(await _movieService.GetBooksAsync(), "BookId", "BookId", movie.BookId);
            ViewData["DirectorId"] = new SelectList(await _movieService.GetDirectorsAsync(), "DirectorId", "DirectorId", movie.DirectorId);
            return View(movie);
        }

        // POST: api/Movie/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,Score,Movietype,Releasedate,Movieduration,Budget,Boxofficerevenue,BookId,DirectorId")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _movieService.UpdateAsync(movie); // Implement UpdateAsync in MovieService
                }
                catch (Exception)
                {
                    if (!await _movieService.MovieExistsAsync(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(await _movieService.GetBooksAsync(), "BookId", "BookId", movie.BookId);
            ViewData["DirectorId"] = new SelectList(await _movieService.GetDirectorsAsync(), "DirectorId", "DirectorId", movie.DirectorId);
            return View(movie);
        }

        // GET: api/Movie/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync((int)id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Filmi getir
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            // Filmi silmeden önce, filmle ilişkili yorumları sil
            var comments = await _commentService.GetCommentsByMovieIdAsync(id);
            foreach (var comment in comments)
            {
                await _commentService.DeleteAsync(comment);
            }

            // Filmi sil
            await _movieService.DeleteAsync(movie);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var upcomingMovies = await _movieService.GetUpcomingMoviesAsync();
            var upcomingMoviesList = upcomingMovies.Take(4); 

            return View(upcomingMoviesList);
        }
    }
}


