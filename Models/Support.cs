
namespace Models;

public partial class Support
{
    public int Id { get; set; }
    public virtual ArticleReaction? ArticleReaction { get; set; }
    public virtual CommentReaction? CommentReaction { get; set; }
}