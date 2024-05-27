
using Models;

namespace IRepositories
{
    public interface IAuthRepository
    {
        public Task<int> CreateAsync(User user);
        public Task<int> UpdateAsync();
        public Task<int> DeleteAsync(string appUserId);
        public Task<AppUser> GetByIdAsync(string appUserId);
        public Task<List<AppUser>> GetAllAsync();
    }
}
