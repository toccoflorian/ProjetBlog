
using DTO.ArticleReactionDTO;
using IRepositories;
using IServices;
using Models;

namespace Services
{
    public class ArticleReactionService(
        ILikeService likeService,
        IArticleReactionRepository articleReactionRepository,
        IDisLikeService disLikeService,
        ISupportService supportService) : IArticleReactionService
    {
        //private readonly IArticleReactionRepository _articleReactionRepository = articleReaction;

        public async Task<ArticleReaction> GetByIdAsync(string reactionId)
        {
            return await articleReactionRepository.GetByIdAsync(reactionId) 
                ?? throw new Exception("Aucune réaction à un article ne correspond !");
        }

        public async Task ToggleLikeArticleAsync(ToggleArticleReactionDTO reactionDTO)
        {
            // concatenation de User.Id + Article.Id pour retrouver le ArticleReaction associé directement
            ArticleReaction? articleReaction = await articleReactionRepository
                .GetByIdAsync(reactionDTO.UserId + reactionDTO.ArticleId);
            if (articleReaction == null)
            {
                await articleReactionRepository.CreateAsync(new ArticleReaction
                {
                    Id = reactionDTO.UserId + reactionDTO.ArticleId,
                    UserId = reactionDTO.UserId,
                    ArticleId = reactionDTO.ArticleId,
                    LikeId = await likeService.LikeAsync()                                // like
                });
                await articleReactionRepository.SaveChangesAsync();
            }
            else
            {
                if (articleReaction.LikeId != null)
                {
                    await likeService.UnLikeAsync((int)articleReaction.LikeId!);          // UnLike
                    // supression en cascade de ArticleReaction 
                }
                else if (articleReaction.DisLikeId != null)
                {
                    await disLikeService.UnDislikeAsync((int)articleReaction.DisLikeId!);
                    await articleReactionRepository.CreateAsync(new ArticleReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.ArticleId,
                        UserId = reactionDTO.UserId,
                        ArticleId = reactionDTO.ArticleId,
                        LikeId = await likeService.LikeAsync()                                 // UnDislike + Like
                    });
                    await articleReactionRepository.SaveChangesAsync();
                }
                else if (articleReaction.SupportId != null)
                {
                    await supportService.UnSupportAsync((int)articleReaction.SupportId!);
                    await articleReactionRepository.CreateAsync(new ArticleReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.ArticleId,
                        UserId = reactionDTO.UserId,
                        ArticleId = reactionDTO.ArticleId,
                        LikeId = await likeService.LikeAsync()                                   // UnSupport + like
                    });
                    await articleReactionRepository.SaveChangesAsync();
                }
            }
        }

        public async Task ToggleDisLikeArticleAsync(ToggleArticleReactionDTO reactionDTO)
        {
            // concatenation de User.Id + Article.Id pour retrouver le ArticleReaction associé directement
            ArticleReaction? articleReaction = await articleReactionRepository
                .GetByIdAsync(reactionDTO.UserId + reactionDTO.ArticleId);
            if (articleReaction == null)
            {
                await articleReactionRepository.CreateAsync(new ArticleReaction                  // dislike
                {
                    Id = reactionDTO.UserId + reactionDTO.ArticleId,
                    UserId = reactionDTO.UserId,
                    ArticleId = reactionDTO.ArticleId,
                    DisLikeId = await disLikeService.DislikeAsync()
                });
                await articleReactionRepository.SaveChangesAsync();
            }
            else
            {
                if (articleReaction.DisLikeId != null)
                {
                    await disLikeService.UnDislikeAsync((int)articleReaction.DisLikeId!);         // UnDislike
                    // supression en cascade de ArticleReaction 
                }
                else if (articleReaction.LikeId != null)
                {
                    await likeService.UnLikeAsync((int)articleReaction.LikeId);                    // UnLike + Dislike
                    await articleReactionRepository.CreateAsync(new ArticleReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.ArticleId,
                        UserId = reactionDTO.UserId,
                        ArticleId = reactionDTO.ArticleId,
                        DisLikeId = await disLikeService.DislikeAsync()
                    });
                    await articleReactionRepository.SaveChangesAsync();
                }
                else if (articleReaction.SupportId != null)
                {

                    await supportService.UnSupportAsync((int)articleReaction.SupportId);          // UnSupport + Dislike
                    await articleReactionRepository.CreateAsync(new ArticleReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.ArticleId,
                        UserId = reactionDTO.UserId,
                        ArticleId = reactionDTO.ArticleId,
                        DisLikeId = await disLikeService.DislikeAsync()
                    });
                    await articleReactionRepository.SaveChangesAsync();
                }
            }
        }

        public async Task ToggleSupportArticleAsync(ToggleArticleReactionDTO reactionDTO)
        {
            // concatenation de User.Id + Article.Id pour retrouver le ArticleReaction associé directement
            ArticleReaction? articleReaction = await articleReactionRepository
                .GetByIdAsync(reactionDTO.UserId + reactionDTO.ArticleId);
            if (articleReaction == null)
            {
                await articleReactionRepository.CreateAsync(new ArticleReaction
                {
                    Id = reactionDTO.UserId + reactionDTO.ArticleId,
                    UserId = reactionDTO.UserId,
                    ArticleId = reactionDTO.ArticleId,
                    SupportId = await supportService.SupportAsync()                  // support
            });
                await articleReactionRepository.SaveChangesAsync();
            }
            else
            {
                if(articleReaction.SupportId != null) 
                {
                    await supportService.UnSupportAsync((int)articleReaction.SupportId!);          // UnSupport
                    // supression en cascade de ArticleReaction 
                }
                else if (articleReaction.DisLikeId != null)
                {
                    await disLikeService.UnDislikeAsync((int)articleReaction.DisLikeId);
                    await articleReactionRepository.CreateAsync(new ArticleReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.ArticleId,
                        UserId = reactionDTO.UserId,
                        ArticleId = reactionDTO.ArticleId,
                        SupportId = await supportService.SupportAsync()                          // UnDislike + Support
                });
                    await articleReactionRepository.SaveChangesAsync();
                }
                else if (articleReaction.LikeId != null)
                {
                    await likeService.UnLikeAsync((int)articleReaction.LikeId);             // UnLike + Support
                    await articleReactionRepository.CreateAsync(new ArticleReaction
                    {
                        Id = reactionDTO.UserId + reactionDTO.ArticleId,
                        UserId = reactionDTO.UserId,
                        ArticleId = reactionDTO.ArticleId,
                        SupportId = await supportService.SupportAsync()
                });
                    await articleReactionRepository.SaveChangesAsync();
                }
            }
        }
    }
}
