using DTO.ArticleReactionDTO;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjetBlogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleReactionController(
        IArticleReactionService articleReactionService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ToggleLikeArticle(int articleId) 
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await articleReactionService.ToggleLikeArticleAsync(
                    new ToggleArticleReactionDTO(articleId, userId));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ToggleDisLikeArticle(int articleId) 
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await articleReactionService.ToggleDisLikeArticleAsync(
                    new ToggleArticleReactionDTO(articleId, userId));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ToggleSupportArticle(int articleId) 
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await articleReactionService.ToggleSupportArticleAsync(
                    new ToggleArticleReactionDTO(articleId, userId));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
