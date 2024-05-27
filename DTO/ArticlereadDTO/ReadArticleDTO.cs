namespace DTO.ArticlereadDTO
{
    public class ReadArticleDTO
    {
        public int ArticleId { get; set; }
        public string UserId { get; set; }

        public ReadArticleDTO(int AricleId, string userId)
        {
            ArticleId = AricleId;
            UserId = userId;
        }
    }
}
