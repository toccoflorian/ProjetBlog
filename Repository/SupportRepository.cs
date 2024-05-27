
using IRepositories;
using Models;

namespace Repositories
{
    public class SupportRepository : ISupportRepository
    {
        private readonly ProjetBlogContext _context;
        public SupportRepository(ProjetBlogContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(Support support)
        {
            await this._context.Supports.AddAsync(support);
        }

        public void Delete(Support support)
        {
            this._context.Supports.Remove(support);
        }

        public async Task<Support?> GetByIdAsync(int supportId)
        {
            return await this._context.Supports.FindAsync(supportId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
