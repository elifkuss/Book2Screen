using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Book2Screen.Models;
using Book2Screen.Services.Abstractions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Book2Screen.Views
{
    public class ActorController : Controller
    {
        private readonly IActorService _actorService;
        private readonly IMovieService _movieService;

        public ActorController(IActorService actorService, IMovieService movieService)
        {
            _actorService = actorService;
            _movieService = movieService;
        }

        // GET: Actor
        public async Task<IActionResult> Index(string searchString)
        {
            var actors = await _actorService.SearchActorsAsync(searchString);

            ViewData["SearchString"] = searchString; 

            return View(actors);
        }


        // GET: Actor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _actorService.GetActorByIdAsync(id.Value);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actor/Create
        public async Task<IActionResult> Create()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            ViewData["MovieId"] = new SelectList(movies, "MovieId", "Title");
            return View();
        }

        // POST: Actor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorsId,Name,Birthdate,MovieId")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                await _actorService.AddActorAsync(actor);
                return RedirectToAction(nameof(Index));
            }
            var movies = await _movieService.GetAllMoviesAsync();
            ViewData["MovieId"] = new SelectList(movies, "MovieId", "Title", actor.MovieId);
            return View(actor);
        }

        // GET: Actor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _actorService.GetActorByIdAsync(id.Value);
            if (actor == null)
            {
                return NotFound();
            }

            var movies = await _movieService.GetAllMoviesAsync();
            ViewData["MovieId"] = new SelectList(movies, "MovieId", "Title", actor.MovieId);
            return View(actor);
        }

        // POST: Actor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActorsId,Name,Birthdate,MovieId")] Actor actor)
        {
            if (id != actor.ActorsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _actorService.UpdateActorAsync(actor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ActorExists(actor.ActorsId))
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
            var movies = await _movieService.GetAllMoviesAsync();
            ViewData["MovieId"] = new SelectList(movies, "MovieId", "Title", actor.MovieId);
            return View(actor);
        }

        // GET: Actor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _actorService.GetActorByIdAsync(id.Value);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _actorService.GetActorByIdAsync(id);
            if (actor != null)
            {
                await _actorService.DeleteActorAsync(actor);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ActorExists(int id)
        {
            return await _actorService.ActorExistsAsync(id);
        }
    }
}
