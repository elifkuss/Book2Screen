using Book2Screen.Models;

namespace Book2Screen.Services.Abstractions
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAndEmailAsync(string name, string mail);
        Task<User> Login(string name, string mail);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> UserExistsAsync(int id);
        
    }
}


