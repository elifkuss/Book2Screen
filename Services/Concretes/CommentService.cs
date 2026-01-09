using System.Linq.Expressions;
using Book2Screen.Models;
using Book2Screen.Repository.Abstractions;

namespace Book2Screen.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;

        public CommentService(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task AddAsync(Comment comment)
        {
            await _commentRepository.AddAsync(comment);
        }

        public async Task AddCommentAsync(int userId, int movieId, string commentText)
        {
            var existingComment = await _commentRepository.GetAsync(c => c.Id == userId && c.MovieId == movieId);
            if (existingComment != null)
            {
                throw new InvalidOperationException("User has already commented on this movie.");
            }

            var comment = new Comment
            {
                Id = userId,
                MovieId = movieId,
                CommentText = commentText,
                CommentId = await GenerateCommentIdAsync(),
            };

            await _commentRepository.AddAsync(comment);
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            return await _commentRepository.GetAsync(c => c.CommentId == commentId);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByMovieIdAsync(int movieId)
        {
            return await _commentRepository.GetAllAsync(c => c.MovieId == movieId);
        }

        public async Task<int> GenerateCommentIdAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            if (!comments.Any())
            {
                return 1;
            }

            var maxId = comments.Max(c => c.CommentId);
            return maxId + 1;
        }

        public async Task UpdateAsync(Comment comment)
        {
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task DeleteAsync(Comment comment)
        {
            await _commentRepository.DeleteAsync(comment);
        }

        public async Task<bool> AnyAsync(Expression<Func<Comment, bool>> predicate)
        {
            return await _commentRepository.AnyAsync(predicate);
        }
    }
}
