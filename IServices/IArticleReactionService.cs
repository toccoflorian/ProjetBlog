
using DTO.ArticleReactionDTO;
using Models;

namespace IServices
{
    public interface IArticleReactionService
    {
        /// <summary>
        /// l'id de ArticleReaction est User.Id + Article.Id
        /// </summary>
        /// <param name="reactionId"></param>
        /// <returns></returns>
        public Task<ArticleReaction> GetByIdAsync(string reactionId);
        public Task ToggleLikeArticleAsync(ToggleArticleReactionDTO reactionDTO);
        public Task ToggleDisLikeArticleAsync(ToggleArticleReactionDTO reactionDTO);
        public Task ToggleSupportArticleAsync(ToggleArticleReactionDTO reactionDTO);
    }
}
