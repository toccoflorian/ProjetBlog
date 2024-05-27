
using DTO.CommentReactionDTO;
using Models;

namespace IServices
{
    public interface ICommentReactionService
    {
        /// <summary>
        /// l'id de CommentReaction est User.Id + Comment.Id
        /// </summary>
        /// <param name="reactionId"></param>
        /// <returns></returns>
        public Task<CommentReaction> GetByIdAsync(string reactionId);
        public Task ToggleLikeCommentAsync(ToggleCommentReactionServiceDTO reactionDTO);
        public Task ToggleDisLikeCommentAsync(ToggleCommentReactionServiceDTO reactionDTO);
        public Task ToggleSupportCommentAsync(ToggleCommentReactionServiceDTO reactionDTO);
    }
}
