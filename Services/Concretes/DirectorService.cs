using Book2Screen.Models;
using Book2Screen.Repository.Abstractions;
using Book2Screen.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book2Screen.Services.Concretes
{
    public class DirectorService : IDirectorService
    {
        private readonly IRepository<Director> _directorRepository;

        public DirectorService(IRepository<Director> directorRepository)
        {
            _directorRepository = directorRepository;
        }

        public async Task<List<Director>> GetAllDirectorsAsync()
        {
            return await _directorRepository.GetAllAsync();
        }

        public async Task<Director> GetDirectorByIdAsync(int id)
        {
            return await _directorRepository.GetByIdAsync(id);
        }

        public async Task AddDirectorAsync(Director director)
        {
            await _directorRepository.AddAsync(director);
        }

        public async Task UpdateDirectorAsync(Director director)
        {
            await _directorRepository.UpdateAsync(director);
        }

        public async Task DeleteDirectorAsync(int id)
        {
            var director = await _directorRepository.GetByIdAsync(id);
            if (director != null)
            {
                await _directorRepository.DeleteAsync(director);
            }
        }

        public async Task<bool> DirectorExistsAsync(int id)
        {
            return await _directorRepository.AnyAsync(d => d.DirectorId == id);
        }

        public async Task<IEnumerable<Director>> SearchDirectorsAsync(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return await _directorRepository.GetAllAsync();
            }

            return await _directorRepository.GetAllAsync(a => a.Name != null && a.Name.Contains(searchString));
        }

    }
}
