
using DTO.CommentReactionDTO;
using IRepositories;
using IServices;
using Models;

namespace Services
{
    public class CommentReactionService(
        ICommentReactionRepository commentReactionRepository,
        ILikeService likeService,
        IDisLikeService disLikeService,
        ISupportService supportService
        ) : ICommentReactionService
    {
        public async Task<CommentReaction> GetByIdAsync(string reactionId)
        {
            return await commentReactionRepository.GetByIdAsync(reactionId)
                ?? throw new NotImplementedException();
        }

        public async Task ToggleLikeCommentAsync(ToggleCommentReactionServiceDTO reactionDTO)
        {
            CommentReaction? reaction = await commentReactionRepository
                .GetByIdAsync(reactionDTO.UserId + reactionDTO.CommentId);
            if(reaction == null)
            {
                await commentReactionRepository.CreateAsync(                // Like comment
                    new CommentReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.CommentId,
                        UserId = reactionDTO.UserId,
                        CommentId = reactionDTO.CommentId,
                        LikeId = await likeService.LikeAsync()
                    });
                await commentReactionRepository.SaveChangesAsync();
            }
            else if(reaction.LikeId != null)                            // UnLike comment
            {
                await likeService.UnLikeAsync((int)reaction.LikeId);
            }
            else if(reaction.DisLikeId != null)                            // UnDislike + Like comment
            {
                await disLikeService.UnDislikeAsync((int)reaction.DisLikeId);
                await commentReactionRepository.CreateAsync(
                    new CommentReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.CommentId,
                        UserId = reactionDTO.UserId,
                        CommentId = reactionDTO.CommentId,
                        LikeId = await likeService.LikeAsync()
                    });
                await commentReactionRepository.SaveChangesAsync();
            }
            else if(reaction.SupportId != null)                         // UnSupport + Like comment
            {
                await supportService.UnSupportAsync((int)reaction.SupportId);
                await commentReactionRepository.CreateAsync(    
                    new CommentReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.CommentId,
                        UserId = reactionDTO.UserId,
                        CommentId = reactionDTO.CommentId,
                        LikeId = await likeService.LikeAsync()
                    });
                await commentReactionRepository.SaveChangesAsync();
            }
        }

        public async Task ToggleDisLikeCommentAsync(ToggleCommentReactionServiceDTO reactionDTO)
        {
            CommentReaction? reaction = await commentReactionRepository
                .GetByIdAsync(reactionDTO.UserId + reactionDTO.CommentId);
            if (reaction == null)
            {
                await commentReactionRepository.CreateAsync(                // Dislike comment
                    new CommentReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.CommentId,
                        UserId = reactionDTO.UserId,
                        CommentId = reactionDTO.CommentId,
                        DisLikeId = await disLikeService.DislikeAsync()
                    });
                await commentReactionRepository.SaveChangesAsync();
            }
            else if (reaction.DisLikeId != null)                            // UnDislike comment
            {
                await disLikeService.UnDislikeAsync((int)reaction.DisLikeId);
            }
            else if (reaction.LikeId != null)                            // Unlike + Dislike comment
            {
                await likeService.UnLikeAsync((int)reaction.LikeId);
                await commentReactionRepository.CreateAsync(
                    new CommentReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.CommentId,
                        UserId = reactionDTO.UserId,
                        CommentId = reactionDTO.CommentId,
                        DisLikeId = await disLikeService.DislikeAsync()
                    });
                await commentReactionRepository.SaveChangesAsync();
            }
            else if (reaction.SupportId != null)                         // UnSupport + Dislike comment
            {
                await supportService.UnSupportAsync((int)reaction.SupportId);
                await commentReactionRepository.CreateAsync(
                    new CommentReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.CommentId,
                        UserId = reactionDTO.UserId,
                        CommentId = reactionDTO.CommentId,
                        DisLikeId = await disLikeService.DislikeAsync()
                    });
                await commentReactionRepository.SaveChangesAsync();
            }
        }

        public async Task ToggleSupportCommentAsync(ToggleCommentReactionServiceDTO reactionDTO)
        {
            CommentReaction? reaction = await commentReactionRepository
                .GetByIdAsync(reactionDTO.UserId + reactionDTO.CommentId);
            if (reaction == null)
            {
                await commentReactionRepository.CreateAsync(                // Support comment
                    new CommentReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.CommentId,
                        UserId = reactionDTO.UserId,
                        CommentId = reactionDTO.CommentId,
                        SupportId = await supportService.SupportAsync()
                    });
                await commentReactionRepository.SaveChangesAsync();
            }
            else if (reaction.SupportId != null)                            // UnSupport comment
            {
                await supportService.UnSupportAsync((int)reaction.SupportId);
            }
            else if (reaction.LikeId != null)                            // Unlike + Support comment
            {
                await likeService.UnLikeAsync((int)reaction.LikeId);
                await commentReactionRepository.CreateAsync(
                    new CommentReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.CommentId,
                        UserId = reactionDTO.UserId,
                        CommentId = reactionDTO.CommentId,
                        SupportId = await supportService.SupportAsync()
                    });
                await commentReactionRepository.SaveChangesAsync();
            }
            else if (reaction.DisLikeId != null)                         // UnDislike + Support comment
            {
                await disLikeService.UnDislikeAsync((int)reaction.DisLikeId);
                await commentReactionRepository.CreateAsync(
                    new CommentReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.CommentId,
                        UserId = reactionDTO.UserId,
                        CommentId = reactionDTO.CommentId,
                        SupportId = await supportService.SupportAsync()
                    });
                await commentReactionRepository.SaveChangesAsync();
            }
        }
    }
}
