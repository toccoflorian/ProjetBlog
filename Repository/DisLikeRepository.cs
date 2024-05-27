
using IRepositories;
using Models;

namespace Repositories
{
    public class DisLikeRepository : IDisLikeRepository
    {
        private readonly ProjetBlogContext _context;
        public DisLikeRepository(ProjetBlogContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(DisLike dislike)
        {
            await this._context.DisLikes.AddAsync(dislike);
        }

        public void Delete(DisLike dislike)
        {
            this._context.DisLikes.Remove(dislike);
        }

        public async Task<DisLike?> GetByIdAsync(int dislikeId)
        {
            return await this._context.DisLikes.FindAsync(dislikeId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
