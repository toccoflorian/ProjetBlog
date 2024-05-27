namespace Models;

public partial class ArticleReaction
{
    public required string Id { get; set; }

    public required string UserId { get; set; }

    public int ArticleId { get; set; }

    public int? SupportId { get; set; }

    public int? DisLikeId { get; set; }

    public int? LikeId { get; set; }

    public virtual Article? Article { get; set; }

    public virtual DisLike? DisLike { get; set; }

    public virtual Like? Like { get; set; }

    public virtual Support? Support { get; set; }

    public virtual User? User { get; set; }
}