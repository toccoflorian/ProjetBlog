
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Tests.Mocks.Repository
{
    internal class ArticleReactionRepositoryMock(ProjetBlogContext inMemoryContext) : IArticleReactionRepository
    {
        public async Task CreateAsync(ArticleReaction reaction)
        {
            await inMemoryContext.ArticleReactions.AddAsync(reaction);
        }

        public void Delete(ArticleReaction reaction)
        {
            inMemoryContext.ArticleReactions.Remove(reaction);
        }

        public async Task<List<ArticleReaction>> GetAllAsync()
        {
            return await inMemoryContext.ArticleReactions.ToListAsync();
        }

        public async Task<ArticleReaction?> GetByIdAsync(string reactionId)
        {
            return await inMemoryContext.ArticleReactions.FindAsync(reactionId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await inMemoryContext.SaveChangesAsync();
        }
    }
}
