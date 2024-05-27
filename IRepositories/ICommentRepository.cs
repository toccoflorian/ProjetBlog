using Models;

namespace IRepositories
{
    public interface ICommentRepository
    {
        public Task CreateAsync(Comment comment);
        public Task UpdateAsync(Comment comment);
        public void Delete(Comment comment);
        public Task<Comment> GetByIdAsync(int commentId);
        public Task<List<Comment>> getAllAsync();
        public Task<int> SaveChangesAsync();
    }
}
