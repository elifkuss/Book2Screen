using Book2Screen.Models;
using Book2Screen.Repository.Abstractions;
using Book2Screen.Services.Abstractions;

namespace Book2Screen.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public int GenerateUniqueUserId()
        {
            Random random = new Random();
            int newId;

            do
            {
                newId = random.Next(1, 100001);
            } while (_userRepository.AnyAsync(u => u.Id == newId).Result);

            return newId;
        }

        public async Task CreateUserAsync(User user)
        {
            user.Id = GenerateUniqueUserId();
            await _userRepository.AddAsync(user);
        }

        public async Task<User> GetUserByUsernameAndEmailAsync(string name, string mail)
        {
            return await _userRepository.GetAsync(u => u.Name == name && u.Mail == mail);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetAsync(u => u.Id == id);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _userRepository.DeleteAsync(user);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _userRepository.AnyAsync(u => u.Id == id);
        }

        public async Task<User> Login(string name, string mail)
        {
            return await GetUserByUsernameAndEmailAsync(name, mail);
        }
    }
}
