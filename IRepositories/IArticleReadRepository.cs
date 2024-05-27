
using DTO.ArticlereadDTO;
using Models;

namespace IRepositories
{
    public interface IArticleReadRepository
    {
        public Task CreateAsync(ArticleRead articleRead);
        public Task<ArticleRead?> GetByIdAsync(string readId);
        public Task<int> SaveChangesAsync();
    }
}
