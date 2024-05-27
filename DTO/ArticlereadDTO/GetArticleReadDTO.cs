
namespace DTO.ArticlereadDTO
{
    public class GetArticleReadDTO
    {
        public string Id { get; set; }
        public int ArticleId { get; set; }
        public string Userid{ get; set; }

        public GetArticleReadDTO(int articleId, string userId) 
        {
            this.Id = userId + articleId;
            this.ArticleId = articleId;
            this.Userid = userId;
        }
    }
}
