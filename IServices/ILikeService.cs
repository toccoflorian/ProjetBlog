
using Models;

namespace IServices
{
    public interface ILikeService
    {
        public Task<int> LikeAsync();
        public Task UnLikeAsync(int likeId);
        public Task<Like?> GetByIdAsync(int likeId);
    }
}
