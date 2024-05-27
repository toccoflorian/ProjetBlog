
using IServices;
using Models;

namespace Tests.Mocks.Service
{
    internal class LikeServiceMock(ProjetBlogContext inMemoryContext) : ILikeService
    {
        public async Task<Like?> GetByIdAsync(int likeId)
        {
            return await inMemoryContext.Likes.FindAsync(likeId);
        }

        public async Task<int> LikeAsync()
        {
            await inMemoryContext.AddAsync(new Like());
            return await inMemoryContext.SaveChangesAsync();
        }

        public async Task UnLikeAsync(int likeId)
        {
            Like? like = await GetByIdAsync(likeId);
            if(like != null)
                inMemoryContext.Likes.Remove(like);
            await inMemoryContext.SaveChangesAsync();
        }
    }
}
