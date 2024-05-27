
using DTO.CategoryDTO;

namespace ViewModels.Article
{
    public class WriteArticleViewModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public int? CategoryId { get; set; }
        public virtual string? ImageURL { get; set; }
        public  List<GetCategoryDTO> Categories { get; set; }

        public WriteArticleViewModel(List<GetCategoryDTO> categories)
        {
            this.Categories = categories;
        }
    }
}
