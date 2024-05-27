using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class Article
{
    public const int TITLE_MIN_LENGTH = 3;
    public const int TITLE_MAX_LENGTH = 150;

    public const int DESCRIPTION_MIN_LENGTH = 35;
    public const int DESCRIPTION_MAX_LENGTH = 250;

    public const int CONTENT_MIN_LENGTH = 100;
    public const int CONTENT_MAX_LENGTH = 2000;

    public int Id { get; set; }

    [MinLength(TITLE_MIN_LENGTH)]
    [MaxLength(TITLE_MAX_LENGTH)]
    [StringLength(TITLE_MAX_LENGTH, MinimumLength = TITLE_MIN_LENGTH)]
    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string Content { get; set; }

    public required string ImageURL { get; set; }

    public required DateTime EditionDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public required int CategoryId { get; set; }

    public virtual Category? Category { get; set; }
    public required string UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User? Author { get; set; }
    public virtual List<ArticleReaction>? ArticleReactions { get; set; }
    public virtual List<Comment>? Comments { get; set; }
    public virtual List<ArticleRead>? ArticleReads { get; set; }
}