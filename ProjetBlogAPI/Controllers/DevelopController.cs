using Microsoft.AspNetCore.Mvc;
using IServices;
using DTO.ArticleDTO;
using System.Security.Claims;
using Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetBlogAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DevelopController(
        //ILogger<DevelopController> logger,
        IArticleService articleService,
        ICategoryService categoryService,
        IArticleReactionService articleReactionService,
        ProjetBlogContext context) : ControllerBase
    {


        [HttpPost]
        public async Task<ActionResult> WriteArticle(WriteArticleRequestDTO articleDTO)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                return BadRequest("Identifiez-vous !");
            }
            await articleService.WriteAsync(new WriteArticleServiceDTO(articleDTO, userId));
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(string categoryLabel)
        {
            await categoryService.CreateAsync(categoryLabel);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<ArticleReaction>> GetArticleReactionById(string reactionId)
        {
            try 
            { 
                return Ok(await context.ArticleReactions
                    .Include(re => re.Article)
                    .Include(react => react.User)
                    .ThenInclude(u => u!.AppUser)
                    .FirstOrDefaultAsync(r=>r.Id == reactionId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ArticleReaction>> GetAllArticleReaction()
        {
            try
            {
                return Ok(await context.ArticleReactions

                    .ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
 