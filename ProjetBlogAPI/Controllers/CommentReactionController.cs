using DTO.CommentReactionDTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Claims;

namespace ProjetBlogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentReactionController(CommentReactionService commentReactionService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> ToggleLikeComment(ToggleCommentReactionRequestDTO reactionRequestDTO)
        {
            try
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new Exception("Identifiez-vous.");
                await commentReactionService.ToggleLikeCommentAsync(
                    new ToggleCommentReactionServiceDTO
                    {
                        CommentId = reactionRequestDTO.CommentId,
                        UserId = userId,
                    });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ToggleDisLikeComment(ToggleCommentReactionRequestDTO reactionRequestDTO)
        {
            try
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new Exception("Identifiez-vous.");
                await commentReactionService.ToggleDisLikeCommentAsync(
                    new ToggleCommentReactionServiceDTO
                    {
                        CommentId = reactionRequestDTO.CommentId,
                        UserId = userId,
                    });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ToggleSupportComment(ToggleCommentReactionRequestDTO reactionRequestDTO)
        {
            try
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new Exception("Identifiez-vous.");
                await commentReactionService.ToggleSupportCommentAsync(
                    new ToggleCommentReactionServiceDTO
                    {
                        CommentId = reactionRequestDTO.CommentId,
                        UserId = userId,
                    });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
