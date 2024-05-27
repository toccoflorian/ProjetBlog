
namespace DTO.ArticleDTO
{
    public class DeleteArticleRequestDTO
    {
        public int ArticleId { get; set; }
    }

    public class DeleteArticleServiceDTO
    {
        public string UserId { get; set; }
        public int ArticleId { get; set; }
        
        public DeleteArticleServiceDTO(DeleteArticleRequestDTO requestDTO, string userId)
        {
            this.UserId = userId;
            this.ArticleId = requestDTO.ArticleId;
        }
    }

    public class DeleteArticleRepositoryDTO
    {
        public int ArticleId { get; set; }

        public DeleteArticleRepositoryDTO(DeleteArticleServiceDTO serviceDTO)
        {
            this.ArticleId= serviceDTO.ArticleId;
        }
    }
}
