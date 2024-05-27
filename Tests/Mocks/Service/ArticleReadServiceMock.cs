
using DTO.ArticlereadDTO;
using IServices;

namespace Tests.Mocks.Service
{
    internal class ArticleReadServiceMock : IArticleReadService
    {
        public Task<int> ArticleReadedAsync(GetArticleReadDTO articleReadDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAllreadyReadedAsync(string readId)
        {
            throw new NotImplementedException();
        }
    }
}
