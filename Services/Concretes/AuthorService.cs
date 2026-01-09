using System.Collections.Generic;
using System.Threading.Tasks;
using Book2Screen.Models;
using Book2Screen.Repository.Abstractions;
using Book2Screen.Services.Abstractions;

namespace Book2Screen.Services.Concretes
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _authorRepository.GetAsync(a => a.AuthorId == id);
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _authorRepository.AddAsync(author);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            await _authorRepository.UpdateAsync(author);
        }

        public async Task DeleteAuthorAsync(Author author)
        {
            await _authorRepository.DeleteAsync(author);
        }

        public async Task<bool> AuthorExistsAsync(int id)
        {
            return await _authorRepository.AnyAsync(a => a.AuthorId == id);
        }

        public async Task<IEnumerable<Author>> SearchAuthorsAsync(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return await _authorRepository.GetAllAsync();
            }

            return await _authorRepository.GetAllAsync(a => a.Name != null && a.Name.Contains(searchString));
        }

    }
}

