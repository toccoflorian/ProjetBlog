
using IHelpers;
using Models;
using System.ComponentModel.DataAnnotations;

namespace Helpers
{
    public class ArticleHelper : IArticleHelper
    {
        public Article Validate(Article article)
        {
            if (article.Title.Length < Article.TITLE_MIN_LENGTH)
                throw new ValidationException("Le titre de l'article (Article.Title) dois contenir au moins 3 caractèrs.");

            if (article.Title.Length > Article.TITLE_MAX_LENGTH)
                throw new ValidationException("Le titre de l'article (Article.Title) dois contenir au maximum 150 caractèrs.");

            if (article.Description.Length < Article.DESCRIPTION_MIN_LENGTH)
                throw new ValidationException("La description (Article.Description) de l'article dois contenir au moins 35 caractèrs.");

            if (article.Description.Length > Article.DESCRIPTION_MAX_LENGTH)
                throw new ValidationException("La description (Article.Description) de l'article dois contenir au maximum 200 caractèrs.");

            if (article.Content.Length < Article.CONTENT_MIN_LENGTH)
                throw new ValidationException("Le contenu (Article.Content) de l'article dois contenir au moins 100 caractèrs.");

            if (article.Content.Length > Article.CONTENT_MAX_LENGTH)
                throw new ValidationException("Le contenu (Article.Content) de l'article dois contenir au maximum 2000 caractèrs.");

            return article;
        }
    }
}
