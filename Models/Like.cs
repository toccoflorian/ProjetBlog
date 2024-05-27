
namespace Models;

public partial class Like
{
    public int Id { get; set; }
    public ArticleReaction? ArticleReaction { get; set; }
    public CommentReaction? CommentReaction { get; set; }
}