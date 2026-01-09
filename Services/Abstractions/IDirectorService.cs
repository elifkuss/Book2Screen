using Book2Screen.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book2Screen.Services.Abstractions
{
    public interface IDirectorService
    {
        Task<List<Director>> GetAllDirectorsAsync();
        Task<Director> GetDirectorByIdAsync(int id);
        Task AddDirectorAsync(Director director);
        Task UpdateDirectorAsync(Director director);
        Task DeleteDirectorAsync(int id);
        Task<bool> DirectorExistsAsync(int id);
        Task<IEnumerable<Director>> SearchDirectorsAsync(string searchString);
    }
}
