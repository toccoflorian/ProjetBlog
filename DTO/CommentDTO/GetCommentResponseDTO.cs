
using Models;

namespace DTO.CommentDTO
{
    public class GetCommentResponseDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime EditionDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleCategory { get; set; }
        public string AuthorFirstname { get; set; }
        public string AuthorLastname { get; set; }
        public int NbLikes { get; set; }
        public int NbDislikes { get; set; }
        public int NbSupports { get; set; }

        public GetCommentResponseDTO(Comment comment)
        {
            Id = comment.Id;
            Content = comment.Content;
            EditionDate = comment.EditionDate;
            UpdatedDate = comment.UpdatedDate;
            ArticleTitle = comment.Article!.Title;
            ArticleCategory = comment.Article.Category!.Label;
            AuthorFirstname = comment.User!.Firstname;
            AuthorLastname = comment.User.Lastname;
            NbLikes = (int)comment.CommentReactions?.Where(reaction => reaction.LikeId != null).Count()!;
            NbDislikes = (int)comment.CommentReactions?.Where(reaction => reaction.DisLikeId != null).Count()!;
            NbSupports = (int)comment.CommentReactions?.Where(reaction => reaction.SupportId != null).Count()!;
        }
    }
}
