
namespace DTO.ArticleReactionDTO
{
    public class ToggleArticleReactionDTO
    {
        public string UserId { get; set; }
        public int ArticleId { get; set; }

        public ToggleArticleReactionDTO(int articleId, string UserId)
        {
            this.UserId = UserId;
            this.ArticleId = articleId;
        }
    }
}
