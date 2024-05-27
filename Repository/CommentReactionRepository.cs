
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class CommentReactionRepository(ProjetBlogContext context) : ICommentReactionRepository
    {
        public async Task CreateAsync(CommentReaction reaction)
        {
            await context.CommentReactions.AddAsync(reaction);
        }

        public void Delete(CommentReaction reaction)
        {
            context.CommentReactions.Remove(reaction);
        }

        public async Task<List<CommentReaction>> GetAllAsync()
        {
            return await context.CommentReactions.ToListAsync();
        }

        public async Task<CommentReaction?> GetByIdAsync(string reactionId)
        {
            return await context.CommentReactions
                .FirstOrDefaultAsync(reaction => reaction.Id == reactionId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
