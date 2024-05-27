

using System.ComponentModel.DataAnnotations;

namespace DTO.ArticleDTO
{
    public class WriteArticleRequestDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Content { get; set; }
        public int CategoryId { get; set; }
        public virtual string? ImageURL { get; set; }
    }

    public class WriteArticleServiceDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public string? ImageURL { get; set; }
        public string UserId { get; set; }
        public WriteArticleServiceDTO(WriteArticleRequestDTO requestDTO, string userId) 
        {
            this.Title = requestDTO.Title;
            this.Description = requestDTO.Description;
            this.Content = requestDTO.Content;
            this.CategoryId = requestDTO.CategoryId;
            this.ImageURL = requestDTO.ImageURL;
            this.UserId = userId;
        }
    }
}

    
