
using IServices;
using Models;

namespace Tests.Mocks.Service
{
    internal class DisLikeServiceMock(ProjetBlogContext inMemoryContext) : IDisLikeService
    {
        public async Task<int> DislikeAsync()
        {
            await inMemoryContext.DisLikes.AddAsync(new DisLike());
            return await inMemoryContext.SaveChangesAsync();
        }

        public async Task<DisLike?> GetByIdAsync(int dislikeId)
        {
            return await inMemoryContext.DisLikes.FindAsync(dislikeId);
        }

        public async Task UnDislikeAsync(int dislikeId)
        {
            DisLike? disLike = await GetByIdAsync(dislikeId);
            if(disLike != null)
                inMemoryContext.DisLikes.Remove(disLike);
            await inMemoryContext.SaveChangesAsync();
        }
    }
}
