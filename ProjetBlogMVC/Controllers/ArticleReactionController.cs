using DTO.ArticleReactionDTO;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjetBlogMVC.Controllers
{
    public class ArticleReactionController(
        IArticleService articleService,
        IArticleReactionService articleReactionService) : Controller
    {
        
        //[HttpGet]
        //public IActionResult Index(int articleId)
        //{
            
        //    return ViewComponent("ArticleReaction");
        //}

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ToggleLike(int articleId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception("Utilisateur non identifier.");

            await articleReactionService.ToggleLikeArticleAsync(
                new ToggleArticleReactionDTO(articleId, userId));

            return ViewComponent("ArticleReaction", await articleService.GetByIdAsync(articleId));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ToggleDislike(int articleId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception("Utilisateur non identifier.");

            await articleReactionService.ToggleDisLikeArticleAsync(
                new ToggleArticleReactionDTO(articleId, userId));

            return ViewComponent("ArticleReaction", await articleService.GetByIdAsync(articleId));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ToggleSupport(int articleId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception("Utilisateur non identifier.");

            await articleReactionService.ToggleSupportArticleAsync(
                new ToggleArticleReactionDTO(articleId, userId));

            return ViewComponent("ArticleReaction", await articleService.GetByIdAsync(articleId));
        }
    }
}
