

using Book2Screen.Models;
using Book2Screen.Repository.Abstractions;
using Book2Screen.Services.Abstractions;

namespace Book2Screen.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Director> _directorRepository;

        public MovieService(
            IRepository<Movie> movieRepository,
            IRepository<Comment> commentRepository,
            IRepository<Book> bookRepository,
            IRepository<Director> directorRepository)
        {
            _movieRepository = movieRepository;
            _commentRepository = commentRepository;
            _bookRepository = bookRepository;
            _directorRepository = directorRepository;
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _movieRepository.GetAsync(m => m.MovieId == id, m => m.Actors , m => m.Director ); 
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsForMovieAsync(int movieId)
        {
            
            return await _commentRepository.GetAllAsync(c => c.MovieId == movieId); 
        }

        public async Task AddAsync(Movie movie)
        {
            await _movieRepository.AddAsync(movie);
        }

        public async Task UpdateAsync(Movie movie)
        {
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task DeleteAsync(Movie movie)
        {
            await _movieRepository.DeleteAsync(movie);
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _commentRepository.AddAsync(comment);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Director>> GetDirectorsAsync()
        {
            return await _directorRepository.GetAllAsync();
        }

        public async Task<bool> MovieExistsAsync(int id)
        {
            return await _movieRepository.AnyAsync(m => m.MovieId == id); 
        }

        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _movieRepository.GetAllAsync();
            }

            return await _movieRepository.GetAllAsync(m => m.Title.ToLower().Contains(searchTerm.ToLower()));
        }

        public async Task<IEnumerable<Movie>> GetUpcomingMoviesAsync()
        {
            var today = DateTime.Now.Date; 
            return await _movieRepository.GetAllAsync(m => m.Releasedate.HasValue);
        }


    }
}

