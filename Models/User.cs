
using Enums;

namespace Models;


public partial class User
{
    public string Id { get; set; }

    public required string Firstname { get; set; }

    public required string Lastname { get; set; }

    public required string ImageUrl { get; set; }

    public UserSexeEnum Sexe { get; set; }

    public DateTime SignInDate { get; set; }
    public required string AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
#nullable enable
    public List<ArticleReaction>? ArticleReactions { get; set; }

    public List<Article>? Articles { get; set; }
#nullable enable
    public List<CommentReaction>? CommentReactions { get; set; }
#nullable enable
    public List<Comment>? Comments { get; set; }
#nullable enable
    public List<ArticleRead>? ArticleReads { get; set; }
}