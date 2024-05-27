using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ICommentReactionRepository
    {
        public Task CreateAsync(CommentReaction reaction);
        public void Delete(CommentReaction reaction);
        public Task<CommentReaction?> GetByIdAsync(string reactionId);
        public Task<List<CommentReaction>> GetAllAsync();
        public Task<int> SaveChangesAsync();
    }
}
