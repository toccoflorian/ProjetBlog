
namespace DTO.CommentReactionDTO
{
    public class ToggleCommentReactionRequestDTO
    {
        public int CommentId { get; set; }
    }

    public class ToggleCommentReactionServiceDTO
    {
        public int CommentId { get; set; }
        public required string UserId { get; set; }
    }
}
