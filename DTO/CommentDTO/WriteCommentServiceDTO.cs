

namespace DTO.CommentDTO
{
    public class WriteCommentRequestDTO
    {
        public required string Content { get; set; }
        public required int ArticleId { get; set; }
    }

    public class WriteCommentServiceDTO
    {
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public string UserId { get; set; }
        public WriteCommentServiceDTO(WriteCommentRequestDTO requestDTO, string userId)
        {
            this.Content = requestDTO.Content;
            this.ArticleId = requestDTO.ArticleId;
            this.UserId = userId;
        }
    }
}
