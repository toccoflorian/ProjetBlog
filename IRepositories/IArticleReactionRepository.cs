using Models;

namespace IRepositories
{
    public interface IArticleReactionRepository
    {
        public Task CreateAsync(ArticleReaction reaction);
        public void Delete(ArticleReaction reaction);
        public Task<ArticleReaction?> GetByIdAsync(string reactionId);
        public Task<List<ArticleReaction>> GetAllAsync();
        public Task<int> SaveChangesAsync();
    }
}
