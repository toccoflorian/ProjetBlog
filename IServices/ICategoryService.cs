

using DTO.CategoryDTO;
using Models;

namespace IServices
{
    public interface ICategoryService
    {
        public Task CreateAsync(string categoryLabel);
        public Task UpdateAsync();
        public Task DeleteAsync(Category category);
        public Task<Category?> GetByIdAsync(int categoryId);
        public Task<List<GetCategoryDTO>> GetAllAsync();
    }
}
