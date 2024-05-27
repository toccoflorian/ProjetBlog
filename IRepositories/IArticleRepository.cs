using DTO.ArticleDTO;
using Models;

namespace IRepositories
{
    public interface IArticleRepository
    {
        public Task<int> SaveChangesAsync();
        public Task CreateAsync(Article article);
        public Task<int> UpdateAsync(UpdateArticleRepositoryDTO updateArticleDTO);
        public Task DeleteAsync(Article article);
        public Task<Article?> GetByIdAsync(int articleId);
        public Task<List<Article>> GetAllAsync();
        public Task<List<Article>> GetByAuthorAsync(string authorId);
    }
}
