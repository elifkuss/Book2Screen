using Book2Screen.Models;

namespace Book2Screen.Services.Abstractions
{
	public interface IBookService
	{
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);
        Task<bool> BookExistsAsync(int id);
        IEnumerable<Author> GetAuthors();
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
    }
}

