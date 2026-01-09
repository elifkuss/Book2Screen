using Book2Screen.Models;

namespace Book2Screen.Services.Abstractions
{
    public interface IMovieService
    {
        Task<Movie> GetByIdAsync(int id);
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<IEnumerable<Comment>> GetCommentsForMovieAsync(int movieId);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(Movie movie);
        Task AddCommentAsync(Comment comment);
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<IEnumerable<Director>> GetDirectorsAsync();
        Task<bool> MovieExistsAsync(int id);
        Task<IEnumerable<Movie>> SearchMoviesAsync(string searchTerm);
        Task<IEnumerable<Movie>> GetUpcomingMoviesAsync();


    }
}



