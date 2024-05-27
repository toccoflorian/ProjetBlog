
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class CommentReaction
{
    public required string Id { get; set; }

    public required string UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User? Author { get; set; }

    public required int CommentId { get; set; }
    public virtual Comment? Comment { get; set; }

    public int? LikeId { get; set; }
    public virtual Like? Like { get; set; }

    public int? DisLikeId { get; set; }
    public virtual DisLike? DisLike { get; set; }

    public int? SupportId { get; set; }
    public virtual Support? Support { get; set; }





}