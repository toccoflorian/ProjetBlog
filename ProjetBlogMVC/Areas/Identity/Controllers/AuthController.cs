using DTO.AuthDTO;
using IServices;
using Microsoft.AspNetCore.Mvc;

namespace ProjetBlogMVC.Areas.Identity.Controllers
{
    public class AuthController(IAuthService authService) : Controller
    {
        [Area("Identity")]
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Message = "Register Get";
            return View();
        }

        [Area("Identity")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                await authService.RegisterAsync(registerDTO);
                return RedirectToAction();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
