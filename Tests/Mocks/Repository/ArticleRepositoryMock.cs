
using DTO.ArticleDTO;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Tests.Mocks.Repository
{
    internal class ArticleRepositoryMock(ProjetBlogContext inMemoryContext) : IArticleRepository
    {
        public async Task CreateAsync(Article article)
        {
            article.Id = 1;
            await inMemoryContext.AddAsync(article);
        }

        public Task DeleteAsync(Article article)
        {
            inMemoryContext.Remove(article);
            return Task.CompletedTask;
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await inMemoryContext.Articles.ToListAsync();
        }

        public Task<List<Article>> GetByAuthorAsync(string authorId)
        {
            return inMemoryContext.Articles
                .Where(article => article.UserId == authorId)
                .ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int articleId)
        {
            return await inMemoryContext.Articles.FindAsync(articleId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await inMemoryContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(UpdateArticleRepositoryDTO updateArticleDTO)
        {
            Article? article = await GetByIdAsync(updateArticleDTO.ArticleId);
            if (article == null)
                return 0;
            article.Title = updateArticleDTO.Title;
            article.Description = updateArticleDTO.Description;
            article.Content = updateArticleDTO.Content;
            article.ImageURL = updateArticleDTO.ImageURL ?? article.ImageURL;
            return await inMemoryContext.SaveChangesAsync();
        }
    }
}
