using DTO.AuthDTO;
using IServices;
using Microsoft.AspNetCore.Mvc;

namespace ProjetBlogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDTO dto)
        {
            try
            {
                await this._authService.RegisterAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
