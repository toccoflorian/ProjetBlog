
using CONST;
using Models;

namespace DTO.ArticleDTO
{
    public class GetArticleResponseDTO
    {
            public int Id { get; set; }
            public string Titre { get; set; }
            public string Description { get; set; }
            public DateTime EditionDate { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public string Content { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string AuthorId { get; set; }
            public string AuthorFirstname { get; set; }
            public string AuthorLastname { get; set; }
            public string? ImageURL { get; set; }
            public int NbComments { get; set; }
            public int NbLikes { get; set; }
            public int NbDisLikes { get; set; }
            public int NbSupports { get; set; }
            public int NbReads { get; set; }

        public GetArticleResponseDTO(Article article)
        {
            this.Id = article.Id;
            this.Titre = article.Title;
            this.Description = article.Description;
            this.Content = article.Content;
            this.EditionDate = article.EditionDate;
            this.UpdatedDate = article.UpdatedDate;
            this.CategoryName = article.Category!.Label;
            this.CategoryId = article.Category.Id;
            this.AuthorId = article.Author!.Id;
            this.AuthorFirstname = article.Author.Firstname;
            this.AuthorLastname = article.Author.Lastname;
            this.ImageURL = article.ImageURL;
            this.NbComments = article.Comments != null ? (int)article.Comments.Count() : 0;
            this.NbLikes = (int)article.ArticleReactions?.Where(reaction => reaction.LikeId != null).Count()!;
            this.NbDisLikes = (int)article.ArticleReactions?.Where(reaction => reaction.DisLikeId != null).Count()!;
            this.NbSupports = (int)article.ArticleReactions?.Where(reaction => reaction.SupportId != null).Count()!;
            this.NbReads = (int)article.ArticleReads?.Count()!;
        }

        public GetArticleResponseDTO(GetArticleDetailResponseDTO getArticleDetailResponseDTO)
        {
            this.Id = getArticleDetailResponseDTO.Id;
            this.Titre = getArticleDetailResponseDTO.Title;
            this.Description = getArticleDetailResponseDTO.Description;
            this.Content = getArticleDetailResponseDTO.Content;
            this.EditionDate = getArticleDetailResponseDTO.EditionDate;
            this.UpdatedDate = getArticleDetailResponseDTO.UpdatedDate;
            this.CategoryName = getArticleDetailResponseDTO.CategoryName;
            this.CategoryId = getArticleDetailResponseDTO.CategoryId;
            this.AuthorId = getArticleDetailResponseDTO.Author!.Id;
            this.AuthorFirstname = getArticleDetailResponseDTO.Author.Firstsname;
            this.AuthorLastname = getArticleDetailResponseDTO.Author.Lastsname;
            this.ImageURL = getArticleDetailResponseDTO.ImageURL;
            this.NbComments = getArticleDetailResponseDTO.NbComments;
            this.NbLikes = getArticleDetailResponseDTO.NbLikes;
            this.NbDisLikes = getArticleDetailResponseDTO.NbDisLikes;
            this.NbSupports = getArticleDetailResponseDTO.NbSupports;
            this.NbReads = getArticleDetailResponseDTO.NbReads;
        }
    }
}
