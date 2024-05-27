using DTO.ArticleDTO;
using DTO.ArticlereadDTO;
using Models;

namespace IServices
{
    public interface IArticleService
    {
        Task WriteAsync(WriteArticleServiceDTO createArticleServiceDTO);
        Task<List<GetArticleResponseDTO>> GetAllAsync();
        Task<GetArticleResponseDTO> GetByIdAsync(int articleId);
        Task<GetArticleDetailResponseDTO> ReadAsync(ReadArticleDTO articleDTO);
        Task<List<GetArticleResponseDTO>> GetByAuthorAsync(string authorId);
        Task<int> UpdateAsync(UpdateArticleServiceDTO updateArticleServiceDTO);
        Task DeleteAsync(DeleteArticleServiceDTO articleId);
    }
}
