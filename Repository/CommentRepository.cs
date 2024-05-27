
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class CommentRepository(ProjetBlogContext context) : ICommentRepository
    {
        public async Task CreateAsync(Comment comment)
        {
            await context.Comments.AddAsync(comment);
        }

        public void Delete(Comment comment)
        {
            context.Comments.Remove(comment);
        }

        public async Task<List<Comment>> getAllAsync()
        {
            return await context.Comments
                .Include(comment => comment.Article!.Category)
                .Include(comment => comment.User)
                .Include(comment => comment.CommentReactions)
                .ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
