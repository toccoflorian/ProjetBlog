
namespace Models;

public partial class Category
{
    public int Id { get; set; }

    public required string Label { get; set; }

    public virtual List<Article>? Articles { get; set; }
}