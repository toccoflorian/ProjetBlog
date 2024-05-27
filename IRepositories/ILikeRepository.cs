using Models;

namespace IRepositories
{
    public interface ILikeRepository
    {
        public Task CreateAsync(Like like);
        public void Delete(Like like);
        public Task<Like?> GetByIdAsync(int likeId);
        public Task<int> SaveChangesAsync();
    }
}
