
using IRepositories;
using Models;

namespace Tests.Mocks.Repository
{
    internal class ArticleReadRepositoryMock : IArticleReadRepository
    {
        public Task CreateAsync(ArticleRead articleRead)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleRead?> GetByIdAsync(string readId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
