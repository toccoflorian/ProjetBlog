
using IHelpers;
using Models;

namespace Tests.Mocks.Helpers
{
    internal class ArticleHelperMock : IArticleHelper
    {
        public Article Validate(Article article)
        {
            return article;
        }
    }
}
