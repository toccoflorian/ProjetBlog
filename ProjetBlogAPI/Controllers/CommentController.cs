using DTO.CommentDTO;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjetBlogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController(
        ICommentService commentService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> WriteComment(WriteCommentRequestDTO requestDTO)
        {

            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await commentService.WriteComment(new WriteCommentServiceDTO(requestDTO, userId));
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<WriteCommentRequestDTO>>> GetAll()
        {
            try
            {
                return Ok(await commentService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
