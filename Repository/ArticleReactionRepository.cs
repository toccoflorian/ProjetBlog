
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class ArticleReactionRepository : IArticleReactionRepository
    {
        private readonly ProjetBlogContext _context;
        public ArticleReactionRepository(ProjetBlogContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(ArticleReaction reaction)
        {
            await this._context.ArticleReactions.AddAsync(reaction);
        }

        public void Delete(ArticleReaction reaction)
        {
            this._context.ArticleReactions.Remove(reaction);
        }

        public async Task<List<ArticleReaction>> GetAllAsync()
        {
            return await this._context.ArticleReactions.ToListAsync();
        }

        public async Task<ArticleReaction?> GetByIdAsync(string reactionId)
        {
            return await this._context.ArticleReactions.FindAsync(reactionId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
