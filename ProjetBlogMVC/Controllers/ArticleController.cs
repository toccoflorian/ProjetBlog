using DTO.ArticleDTO;
using DTO.ArticlereadDTO;
using DTO.CategoryDTO;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using ViewModels.Article;

namespace ProjetBlogMVC.Controllers
{
    public class ArticleController(
        IArticleService articleService,
        ICategoryService categoryService
        ) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Read(int articleId)
        {
            try
            {string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return userId == null ?
                    RedirectToAction("Login", "Account", new { area = "Identity" })
                    :
                    View(await articleService.ReadAsync(
                        new ReadArticleDTO(articleId, userId)));
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<GetArticleResponseDTO>>> GetByAuthor()
        {
            try
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new Exception("Utilisateur non connecté.");
                return View(await articleService.GetByAuthorAsync(userId));
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Write()
        {
            List<GetCategoryDTO> categories = await categoryService.GetAllAsync();
            return View(new WriteArticleViewModel(categories));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Write(WriteArticleRequestDTO requestDTO)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await articleService.WriteAsync(new WriteArticleServiceDTO(requestDTO, userId));
            return RedirectToAction("GetByAuthor");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<GetArticleResponseDTO>> Update(int articleId)
        {
            try
            {
                return View(await articleService.GetByIdAsync(articleId));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                ViewBag.InnerMessage = ex.InnerException?.Message;
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GetArticleResponseDTO>> Update(UpdateArticleRequestDTO updateArticleRequestDTO)
        {
            try
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new Exception("utilisateur non connecté");
                int result = await articleService.UpdateAsync(new UpdateArticleServiceDTO(updateArticleRequestDTO, userId));

                ViewBag.Message = result == -1 ?
                    "Aucune modifications car tous les champs sont identiques à l'original."
                    :
                    "Article Modifié avec succès.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                ViewBag.InnerMessage = ex.InnerException?.Message;
                return View();
            }
        }

        public async Task<ActionResult> Delete(DeleteArticleRequestDTO requestDTO)
        {
            try
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new Exception("Utilisateur non connecté");
                await articleService.DeleteAsync(new DeleteArticleServiceDTO(requestDTO, userId));
                TempData["Message"] = "Article supprimer avec succès.";
                return RedirectToAction("GetByAuthor");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["InnerMessage"] = ex.InnerException?.Message;
                return RedirectToAction("GetByAuthor");
            }
        }
    }
}
