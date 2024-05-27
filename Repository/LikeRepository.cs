
using IRepositories;
using Models;

namespace Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ProjetBlogContext _context;
        public LikeRepository(ProjetBlogContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(Like like)
        {
            await this._context.Likes.AddAsync(like);
        }

        public void Delete(Like like)
        {
            this._context.Likes.Remove(like);
        }

        public async Task<Like?> GetByIdAsync(int likeId)
        {
            return await this._context.Likes.FindAsync(likeId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
