using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models;

public partial class ProjetBlogContext : IdentityDbContext<AppUser>
{

    public ProjetBlogContext(DbContextOptions options): base(options){}

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleReaction> ArticleReactions { get; set; }
    public virtual DbSet<ArticleRead> ArticleReads { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentReaction> CommentReactions { get; set; }

    public virtual DbSet<DisLike> DisLikes { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Support> Supports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ArticleRead>()
            .HasOne(read => read.User)
            .WithMany(user => user.ArticleReads)
            .HasForeignKey(read => read.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<ArticleRead>()
            .HasOne(read => read.Article)
            .WithMany(article => article.ArticleReads)
            .HasForeignKey(read => read.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(comment => comment.User)
            .WithMany(user => user.Comments)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CommentReaction>()
            .HasOne(reaction => reaction.Author)
            .WithMany(user => user.CommentReactions)
            .HasForeignKey(reaction => reaction.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Article>()
            .HasMany(article => article.ArticleReads)
            .WithOne(read => read.Article)
            .HasForeignKey(read => read.ArticleId);

        modelBuilder.Entity<ArticleReaction>()
            .HasOne(reaction => reaction.Like)
            .WithOne(like => like.ArticleReaction)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ArticleReaction>()
            .HasOne(reaction => reaction.DisLike)
            .WithOne(DisLike => DisLike.ArticleReaction)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ArticleReaction>()
            .HasOne(reaction => reaction.Support)
            .WithOne(Support => Support.ArticleReaction)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ArticleReaction>()
            .HasOne(reaction => reaction.User)
            .WithMany(user => user.ArticleReactions)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CommentReaction>()
            .HasOne(reaction => reaction.Like)
            .WithOne(like => like.CommentReaction)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<CommentReaction>()
            .HasOne(reaction => reaction.DisLike)
            .WithOne(like => like.CommentReaction)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<CommentReaction>()
            .HasOne(reaction => reaction.Support)
            .WithOne(like => like.CommentReaction)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Category>().HasData(
            new Category { Label = "Actualié", Id = 1},
            new Category { Label = "Informatique", Id = 2},
            new Category { Label = "Sport", Id = 3},
            new Category { Label = "Cinéma", Id = 4});

        base.OnModelCreating(modelBuilder);
    }

}