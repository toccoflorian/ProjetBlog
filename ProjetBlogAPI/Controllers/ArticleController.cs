using DTO.ArticleDTO;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjetBlogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ArticleController(
        IArticleService articleService) : ControllerBase
    {
        /// <summary>
        /// test comment
        /// </summary>
        /// <param name="articleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        // [Authorize]
        
        public async Task<ActionResult> Write(WriteArticleRequestDTO articleDTO)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await articleService.WriteAsync(new WriteArticleServiceDTO(articleDTO, "696b55b0-4f65-4d28-b3a1-1bd4ba78c44a"));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<List<GetArticleResponseDTO>>> GetAll()
        {
            try
            {
                return Ok(await articleService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<List<GetArticleDetailResponseDTO>>> Read(int articleId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                return Ok(await articleService.GetByIdAsync(articleId));
                //return Ok(await this._articleService.ReadAsync(new ReadArticleDTO(articleId, userId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
