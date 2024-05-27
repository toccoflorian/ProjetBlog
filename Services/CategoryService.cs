using DTO.CategoryDTO;
using IRepositories;
using IServices;
using Models;

namespace Services
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        public async Task CreateAsync(string categoryLabel)
        {
            await categoryRepository.CreateAsync(categoryLabel);
            if (await categoryRepository.SaveChangesAsync() == 0)
            {
                throw new Exception("Une erreur est survenue, catégorie non crée !");
            }
        }

        public Task DeleteAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetCategoryDTO>> GetAllAsync()
        {
            return (await categoryRepository.GetAllAsync())
                .Select(category =>
                    new GetCategoryDTO(category))
                .ToList();
        }

        public Task<Category?> GetByIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
