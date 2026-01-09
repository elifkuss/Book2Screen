using System.Linq.Expressions;
using System.Threading.Tasks;
using Book2Screen.Models;

namespace Book2Screen.Services
{
    public interface ICommentService
    {
        Task AddAsync(Comment comment);
        Task AddCommentAsync(int userId, int movieId, string commentText);
        Task<Comment> GetCommentByIdAsync(int commentId);
        Task<IEnumerable<Comment>> GetCommentsByMovieIdAsync(int movieId);
        Task<int> GenerateCommentIdAsync();
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(Comment comment);
        Task<bool> AnyAsync(Expression<Func<Comment, bool>> predicate);
    }
}
