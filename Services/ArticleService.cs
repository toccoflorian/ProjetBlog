using DTO.ArticleDTO;
using DTO.ArticlereadDTO;
using IHelpers;
using IRepositories;
using IServices;
using Models;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class ArticleService(
        IArticleRepository articleRepository,
        IArticleReadService articleReadService,
        IArticleHelper articleHelper
        ) : IArticleService



    {
        public async Task WriteAsync(WriteArticleServiceDTO articleDTO)     // Write Article
        {
            await articleRepository.CreateAsync(articleHelper.Validate(new Article
            {
                Title = articleDTO.Title,
                Description = articleDTO.Description,
                Content = articleDTO.Content,
                UserId = articleDTO.UserId,
                CategoryId = articleDTO.CategoryId,
                ImageURL = articleDTO.ImageURL ?? CONST.ARTICLE.DEFAULT_IMAGE_URL,
                EditionDate = DateTime.Now,
                UpdatedDate = null,
            }));
            await articleRepository.SaveChangesAsync();
        }

        public async Task<List<GetArticleResponseDTO>> GetAllAsync()        // Get All Articles
        {
            return (await articleRepository.GetAllAsync())
                .Select(article => new GetArticleResponseDTO(article))
                .ToList();
        }

        public async Task<GetArticleResponseDTO> GetByIdAsync(int articleId)        // Get Aricle By Id 
        {
            Article? article = await articleRepository.GetByIdAsync(articleId);
            if (article == null)
            {
                throw new Exception("Aucun article trouvé.");
            }
            return new GetArticleResponseDTO(article);
        }

        public async Task<GetArticleDetailResponseDTO> ReadAsync(ReadArticleDTO articleDTO)         // Read Article
        {
            Article? article = await articleRepository.GetByIdAsync(articleDTO.ArticleId);
            if (article == null)
            {
                throw new Exception("Aucun article trouvé.");
            }

            // si l'article est déjà lu, retourne l'article directement
            if(await articleReadService.IsAllreadyReadedAsync(articleDTO.UserId + article.Id))
            {
                return new GetArticleDetailResponseDTO(article);
            }

            // Enregistrer la lecture de l'article
            int articleReadCreated = await articleReadService
                .ArticleReadedAsync(
                    new GetArticleReadDTO(article.Id, articleDTO.UserId));

            // si le lecture de l'article n'est pas effective 
            if(articleReadCreated == 0)
            {
                throw new Exception("Lecture de l'article non-enregistré !");
            }
            return new GetArticleDetailResponseDTO(article);
        }

        public async Task<List<GetArticleResponseDTO>> GetByAuthorAsync(string authorId)
        {
            return (await articleRepository.GetByAuthorAsync(authorId))
                .Select(article => new GetArticleResponseDTO(article))
                .ToList();
        }

        public async Task<int> UpdateAsync(UpdateArticleServiceDTO updateArticleServiceDTO)
        {
            if(!await this.IsAuthorAsync(updateArticleServiceDTO.AuthorId, updateArticleServiceDTO.ArticleId))
            {
                throw new Exception("L'utilisateur n'est pas l'auteur de l'article.");
            }
            int result = await articleRepository.UpdateAsync(new UpdateArticleRepositoryDTO(updateArticleServiceDTO));
            if (result == 0)
                throw new Exception("Erreur, la modification n'as pas pris effet.");
            return result;
        }

        public async Task<bool> IsAuthorAsync(string userId, int articleId) =>
            (await GetByIdAsync(articleId)).AuthorId == userId;

        public async Task DeleteAsync(DeleteArticleServiceDTO deleteArticleDTO)
        {
            Article article = await articleRepository.GetByIdAsync(deleteArticleDTO.ArticleId)
                ?? throw new Exception("Aucun article ne correspond.");
            if (!await this.IsAuthorAsync(deleteArticleDTO.UserId, deleteArticleDTO.ArticleId))
                throw new Exception("L'utilisateur n'est pas l'auteur de l'article.");

            await articleRepository.DeleteAsync(article);
            if(await articleRepository.SaveChangesAsync() == 0)
                throw new Exception("L'article n'as pas pu être supprimer.");
        }

        
    }
}
