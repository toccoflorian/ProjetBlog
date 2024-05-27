using DTO.ArticleDTO;
using DTO.ArticleReactionDTO;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System.Security.Claims;

namespace ProjetBlogMVC.ViewComponents
{
    public class ArticleReactionViewComponent(IArticleService articleService) : ViewComponent
    {
        [Authorize]
        public IViewComponentResult Invoke(GetArticleResponseDTO article) => View(article);

    }
}
