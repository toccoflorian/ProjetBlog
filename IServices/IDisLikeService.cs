
using Models;

namespace IServices
{
    public interface IDisLikeService
    {
        public Task<int> DislikeAsync();
        public Task UnDislikeAsync(int dislikeId);
        public Task<DisLike?> GetByIdAsync(int dislikeId);
    }
}
