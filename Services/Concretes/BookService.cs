using Book2Screen.Models;
using Book2Screen.Repository.Abstractions;
using Book2Screen.Services.Abstractions;


namespace Book2Screen.Services.Concretes
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;

        public BookService(IRepository<Book> bookRepository, IRepository<Author> authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetAsync(b => b.BookId == id, b => b.Author);
        }

        public async Task AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(Book book)
        {
            await _bookRepository.DeleteAsync(book);
        }

        public async Task<bool> BookExistsAsync(int bookId)
        {
            return await _bookRepository.AnyAsync(b => b.BookId == bookId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _authorRepository.GetAllAsync().Result;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _bookRepository.GetAllAsync();
            }

            return await _bookRepository.GetAllAsync(b => b.Name.ToLower().Contains(searchTerm.ToLower()));
        }
    }
}
