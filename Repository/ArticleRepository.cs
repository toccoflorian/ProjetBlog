using DTO.ArticleDTO;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Models;

namespace Repositories
{
    public class ArticleRepository(ProjetBlogContext context) : IArticleRepository
    {
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task CreateAsync(Article article)
        {
            await context.Articles.AddAsync(article);
        }


        public Task DeleteAsync(Article article)
        {
            context.Articles.Remove(article);
            return Task.CompletedTask;
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await context.Articles
                .Include(article => article.Category)
                .Include(article => article.Author)
                .Include(article => article.Comments)
                .Include(article => article.ArticleReactions)
                .Include(article => article.ArticleReads)
                .ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int articleId)
        {
            return await context.Articles
                .Include(article => article.Category)
                .Include(article => article.Author)
                .Include(article => article.ArticleReactions)
                .Include(article => article.Comments)!
                .ThenInclude(comment => comment.CommentReactions)
                .Include(article => article.Comments)!
                .ThenInclude(comment => comment.User)
                .Include(article => article.ArticleReads)
                .FirstOrDefaultAsync(article => article.Id == articleId);
        }

        public async Task<int> UpdateAsync(UpdateArticleRepositoryDTO updateArticleDTO)
        {
            Article? article = await context.Articles.FindAsync(updateArticleDTO.ArticleId)
                ?? throw new Exception("L'article n'existe pas.");

            if 
            (
                article.Title == updateArticleDTO.Title & 
                article.Description == updateArticleDTO.Description &
                article.Content == updateArticleDTO.Content &
                article.ImageURL == updateArticleDTO.ImageURL
            )
                return -1;

            if(article.Title != updateArticleDTO.Title)
                article.Title = updateArticleDTO.Title;

            if(article.Description != updateArticleDTO.Description)
                article.Description = updateArticleDTO.Description;

            if(article.Content != updateArticleDTO.Content)
                article.Content = updateArticleDTO.Content;

            if (article.ImageURL != updateArticleDTO.ImageURL)
                 article.ImageURL = updateArticleDTO.ImageURL;

            article.UpdatedDate = DateTime.Now.ToUniversalTime();

            return await context.SaveChangesAsync();
        }

        public async Task<List<Article>> GetByAuthorAsync(string authorId)
        {
            return await context.Articles
                .Include(article => article.Category)
                .Include(article => article.Author)
                .Include(article => article.Comments)
                .Include(article => article.ArticleReactions)
                .Include(article => article.ArticleReads)
                .Where(article => article.UserId == authorId)
                .ToListAsync();
        }
    }
}
