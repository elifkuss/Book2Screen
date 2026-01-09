using System.Collections.Generic;
using System.Threading.Tasks;
using Book2Screen.Models;

namespace Book2Screen.Services.Abstractions
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(Author author);
        Task<bool> AuthorExistsAsync(int id);
        Task<IEnumerable<Author>> SearchAuthorsAsync(string searchString);
    }
}

