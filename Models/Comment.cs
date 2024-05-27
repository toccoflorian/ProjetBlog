
namespace Models;

public partial class Comment
{
    public int Id { get; set; }

    public required string Content { get; set; }

    public DateTime EditionDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int ArticleId { get; set; }
    public virtual Article? Article { get; set; }

    public  required string UserId { get; set; }
    public  User? User { get; set; }

    public virtual List<CommentReaction>? CommentReactions { get; set; }

}