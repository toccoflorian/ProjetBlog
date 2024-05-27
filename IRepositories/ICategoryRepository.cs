using Models;


namespace IRepositories
{
    public interface ICategoryRepository
    {
        public Task CreateAsync(string categoryLabel);
        public Task UpdateAsync();
        public Task DeleteAsync(Category category);
        public Task<Category?> GetByIdAsync(int categoryId);
        public Task<List<Category>> GetAllAsync();
        public Task<int> SaveChangesAsync();
    }
}
