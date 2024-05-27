
namespace DTO.ArticleDTO
{
    public class UpdateArticleRequestDTO
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string? ImageURL { get; set; }

        public UpdateArticleRequestDTO() { }
        public UpdateArticleRequestDTO(string title, string description, string content, string? imageURL) 
        {
            this.Title = title;
            this.Description = description;
            this.Content = content;
            this.ImageURL = imageURL;
        }
    }

    public class UpdateArticleServiceDTO
    {
        public int ArticleId { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string? ImageURL { get; set; }

        public UpdateArticleServiceDTO(UpdateArticleRequestDTO updateArticleRequestDTO, string authorId)
        {
            this.ArticleId = updateArticleRequestDTO.ArticleId;
            this.AuthorId = authorId;
            this.Title = updateArticleRequestDTO.Title;
            this.Description = updateArticleRequestDTO.Description;
            this.Content = updateArticleRequestDTO.Content;
            this.ImageURL = updateArticleRequestDTO.ImageURL;
        }
    }

    public class UpdateArticleRepositoryDTO
    {
        public int ArticleId { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string? ImageURL { get; set; }

        public UpdateArticleRepositoryDTO(UpdateArticleServiceDTO updateArticleServiceDTO)
        {
            this.ArticleId = updateArticleServiceDTO.ArticleId;
            this.AuthorId = updateArticleServiceDTO.AuthorId;
            this.Title = updateArticleServiceDTO.Title;
            this.Description = updateArticleServiceDTO.Description;
            this.Content = updateArticleServiceDTO.Content;
            this.ImageURL = updateArticleServiceDTO.ImageURL;
        }
    }
}
