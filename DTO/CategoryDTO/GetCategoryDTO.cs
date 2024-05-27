
using Models;
using System.ComponentModel.DataAnnotations;

namespace DTO.CategoryDTO
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public GetCategoryDTO(Category category) 
        {
            this.Id = category.Id;
            this.Label = category.Label;
        }
    }
}
