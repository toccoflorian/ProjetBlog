using IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class CategoryRepository(ProjetBlogContext context) : ICategoryRepository
    {
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task CreateAsync(string categoryLabel)
        {
            await context.AddAsync(
                new Category
                {
                    Label = categoryLabel
                });
        }

        public Task DeleteAsync(Category category)
        {
            context.Categories.Remove(category);
            return Task.CompletedTask;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int articleId)
        {
            return await context.Categories.FindAsync(articleId);
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
