
using IRepositories;
using Models;

namespace Repositories
{
    public class ArticleReadRepository : IArticleReadRepository
    {
        private readonly ProjetBlogContext _context;
        public ArticleReadRepository(ProjetBlogContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(ArticleRead articleRead)
        {
            await this._context.ArticleReads.AddAsync(articleRead);
        }

        public async Task<ArticleRead?> GetByIdAsync(string readId)
        {
            return await this._context.ArticleReads.FindAsync(readId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
