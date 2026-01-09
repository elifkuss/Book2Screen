using Book2Screen.Models;
using Book2Screen.Services.Abstractions;
using Book2Screen.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Book2Screen.Views
{
    public class DirectorController : Controller
    {
        private readonly IDirectorService _directorService;

        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        // GET: Director
        // GET: Actor
        public async Task<IActionResult> Index(string searchString)
        {
            var actors = await _directorService.SearchDirectorsAsync(searchString);

            ViewData["SearchString"] = searchString;

            return View(actors);
        }


        // GET: Director/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _directorService.GetDirectorByIdAsync(id.Value);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // GET: Director/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Director/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DirectorId,Name,Birthdate,Works")] Director director)
        {
            if (ModelState.IsValid)
            {
                await _directorService.AddDirectorAsync(director);
                return RedirectToAction(nameof(Index));
            }
            return View(director);
        }

        // GET: Director/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _directorService.GetDirectorByIdAsync(id.Value);
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        // POST: Director/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DirectorId,Name,Birthdate,Works")] Director director)
        {
            if (id != director.DirectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _directorService.UpdateDirectorAsync(director);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _directorService.DirectorExistsAsync(director.DirectorId))
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
            return View(director);
        }

        // GET: Director/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _directorService.GetDirectorByIdAsync(id.Value);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // POST: Director/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var director = await _directorService.GetDirectorByIdAsync(id);
            if (director != null)
            {
                await _directorService.DeleteDirectorAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DirectorExists(int id)
        {
            return await _directorService.DirectorExistsAsync(id);
        }
    }
}
