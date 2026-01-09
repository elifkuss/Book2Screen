using Microsoft.AspNetCore.Mvc;
using Book2Screen.Models;
using Book2Screen.Services;
using System.Security.Claims;

namespace Book2Screen.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> AddComment(int movieId, string commentText)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("SignUp", "Account");
            }

            var comment = new Comment
            {
                CommentText = commentText,
                MovieId = movieId,
                Id = int.Parse(userId) // Kullanıcı ID'sini tam sayı olarak ayarla
            };

            await _commentService.AddCommentAsync((int)comment.Id, (int)comment.MovieId, comment.CommentText);
            return RedirectToAction("Details", "Movie", new { id = movieId });
        }

        public async Task<IActionResult> MovieDetails(int id)
        {
            var movieComments = await _commentService.GetCommentsByMovieIdAsync(id);
            if (movieComments == null)
            {
                return NotFound();
            }

            return View(movieComments);
        }

        public async Task<IActionResult> Index()
        {
            var comments = await _commentService.GetCommentsByMovieIdAsync(0); // Tüm yorumları almak için veya ihtiyaçlarınıza göre uyarlayın
            return View(comments);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _commentService.GetCommentByIdAsync((int)id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignUp", "Account");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            comment.Id = int.Parse(userId);
            comment.CommentId = await _commentService.GenerateCommentIdAsync();
            await _commentService.AddAsync(comment);

            return RedirectToAction("Details", "Movie", new { id = comment.MovieId });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _commentService.GetCommentByIdAsync((int)id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,CommentText,MovieId,BookId,Id")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _commentService.UpdateAsync(comment);
                }
                catch (Exception)
                {
                    if (!await _commentService.AnyAsync(c => c.CommentId == comment.CommentId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _commentService.GetCommentByIdAsync((int)id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment != null)
            {
                await _commentService.DeleteAsync(comment);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
