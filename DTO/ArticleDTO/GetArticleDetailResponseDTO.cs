
using DTO.CommentDTO;
using DTO.UserDTO;
using Models;

namespace DTO.ArticleDTO
{
    public class GetArticleDetailResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public GetUserResponseDTO Author { get; set; }
        public string ImageURL { get; set; }
        public DateTime EditionDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int NbComments { get; set; }
        public List<GetCommentResponseDTO>? Comments {get; set;}
        public int NbLikes { get; set; }
        public int NbDisLikes { get; set; }
        public int NbSupports { get; set; }
        public int NbReads { get; set; }

        public GetArticleDetailResponseDTO(Article article)
        {
            this.Id = article.Id;
            this.Title = article.Title;
            this.Description = article.Description;
            this.Content = article.Content;
            this.CategoryId = article.CategoryId;
            this.CategoryName = article.Category!.Label;
            this.Author = new GetUserResponseDTO(article.Author!);
            this.ImageURL = article.ImageURL;
            this.EditionDate = article.EditionDate;
            this.UpdatedDate = article.UpdatedDate;
            this.NbComments = (int)article.Comments?.Count()!;
            this.Comments = article.Comments?.Select(comment => new GetCommentResponseDTO(comment)).ToList();
            this.NbLikes = (int)article.ArticleReactions?.Where(reaction => reaction.LikeId != null).Count()!;
            this.NbDisLikes = (int)article.ArticleReactions?.Where(reaction => reaction.DisLikeId != null).Count()!;
            this.NbSupports = (int)article.ArticleReactions?.Where(reaction => reaction.SupportId != null).Count()!;
            this.NbReads = (int)article.ArticleReads?.Where(read => read.ArticleId == article.Id).Count()!;
        }
    }
}
