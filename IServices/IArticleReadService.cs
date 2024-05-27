
using DTO.ArticleDTO;
using DTO.ArticlereadDTO;

namespace IServices
{
    public interface IArticleReadService
    {
        public Task<int> ArticleReadedAsync(GetArticleReadDTO articleReadDTO);
        public Task<bool> IsAllreadyReadedAsync(string readId);
    }
}
