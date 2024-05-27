
using DTO.ArticlereadDTO;
using IRepositories;
using IServices;
using Models;

namespace Services
{
    public class ArticleReadService(IArticleReadRepository articleReadRepository) : IArticleReadService
    {
        public async Task<int> ArticleReadedAsync(GetArticleReadDTO articleReadDTO)
        {
            await articleReadRepository.CreateAsync(new ArticleRead
            {
                // l'id est formé à la création du DTO en concatenant User.Id + Article.Id
                Id = articleReadDTO.Id,
                ArticleId = articleReadDTO.ArticleId,
                UserId = articleReadDTO.Userid,
                Date = DateTime.Now,
            });
            return await articleReadRepository.SaveChangesAsync();
        }


        public async Task<bool> IsAllreadyReadedAsync(string readId)
        {
            if (await articleReadRepository.GetByIdAsync(readId) == null)
                return false;
            return true;
        }
    }
}
