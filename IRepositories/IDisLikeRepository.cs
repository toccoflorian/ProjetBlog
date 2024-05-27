
using Models;

namespace IRepositories
{
    public interface IDisLikeRepository
    {
        public Task CreateAsync(DisLike dislike);
        public void Delete(DisLike dislike);
        public Task<DisLike?> GetByIdAsync(int dislikeId);
        public Task<int> SaveChangesAsync();
    }
}
