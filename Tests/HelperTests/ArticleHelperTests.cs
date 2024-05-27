
using DTO.ArticleDTO;
using Helpers;
using IHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;

namespace HelperTests
{
    [TestClass()]
    public class ArticleHelperTests
    {
        private  ProjetBlogContext _inMemoryContext;
        private  IServiceProvider _serviceProvider;
        private Article _defaultCorrectArticle;
        private IArticleHelper _articleHelper;

        [TestInitialize] 
        public void Initialize() 
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<ProjetBlogContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));
            services.AddScoped<IArticleHelper, ArticleHelper>();
            _serviceProvider = services.BuildServiceProvider();
            _inMemoryContext = _serviceProvider.GetService<ProjetBlogContext>();
            _articleHelper = new ArticleHelper();
            _defaultCorrectArticle = new Article
            {
                Id = 1,
                CategoryId = 1,
                Title = new string('a', Article.TITLE_MIN_LENGTH + 1),
                Description = new string('a', Article.DESCRIPTION_MIN_LENGTH + 1),
                Content = new string('a', Article.CONTENT_MIN_LENGTH + 1),
                ImageURL = CONST.ARTICLE.DEFAULT_IMAGE_URL,
                EditionDate = DateTime.Now,
                UserId = "connected user id"
            };
        }

        [TestMethod]
        public void Validate_TitleTooShort_Test()
        {
            // ARANGE
            string initialTitle = new string('a', Article.TITLE_MIN_LENGTH - 1);
            _defaultCorrectArticle.Title = initialTitle;

            // ACT
            Action action = () => _articleHelper.Validate(_defaultCorrectArticle);

            // ASSERT
            Assert.ThrowsException<ValidationException>(action);
            Assert.IsTrue(_defaultCorrectArticle.Title.Length < Article.TITLE_MIN_LENGTH);
        }

        [TestMethod]
        public void Validate_TitleTooLong_Test()
        {
            // ARANGE
            string initialTitle = new string('a', Article.TITLE_MAX_LENGTH + 1);
            _defaultCorrectArticle.Title = initialTitle;

            // ACT
            Action action = () => _articleHelper.Validate(_defaultCorrectArticle);

            // ASSERT
            Assert.ThrowsException<ValidationException>(action);
            Assert.IsTrue(_defaultCorrectArticle.Title.Length > Article.TITLE_MAX_LENGTH);
        }

        [TestMethod]
        public void Validate_TitleCorrect_Test()
        {
            // ARANGE
            string initialTitle = new string('a', Article.TITLE_MIN_LENGTH + 1);
            _defaultCorrectArticle.Title = initialTitle;

            // ACT
            Article article = _articleHelper.Validate(_defaultCorrectArticle);

            // ASSERT
            Assert.AreSame(_defaultCorrectArticle, article);
            Assert.IsTrue(_defaultCorrectArticle.Title.Length >= Article.TITLE_MIN_LENGTH 
                && _defaultCorrectArticle.Title.Length <= Article.TITLE_MAX_LENGTH);
        }

        [TestMethod]
        public void Validate_DescriptionTooShort_Test()
        {
            // ARANGE
            string initialDescription = new string('a', Article.DESCRIPTION_MIN_LENGTH - 1);
            _defaultCorrectArticle.Description = initialDescription;

            // ACT
            Action action = () => _articleHelper.Validate(_defaultCorrectArticle);

            // ASSERT
            Assert.ThrowsException<ValidationException>(action);
            Assert.IsTrue(_defaultCorrectArticle.Description.Length < Article.DESCRIPTION_MIN_LENGTH);
        }

        [TestMethod]
        public void Validate_DescriptionTooLong_Test()
        {
            // ARANGE
            string initialDescription = new string('a', Article.DESCRIPTION_MAX_LENGTH + 1);
            _defaultCorrectArticle.Description = initialDescription;

            // ACT
            Action action = () => _articleHelper.Validate(_defaultCorrectArticle);

            // ASSERT
            Assert.ThrowsException<ValidationException>(action);
            Assert.IsTrue(_defaultCorrectArticle.Description.Length > Article.DESCRIPTION_MAX_LENGTH);
        }

        [TestMethod]
        public void Validate_DescriptionCorrect_Test()
        {
            // ARANGE
            string initialDescription = new string('a', Article.DESCRIPTION_MIN_LENGTH + 1);
            _defaultCorrectArticle.Description = initialDescription;

            // ACT
            Article article = _articleHelper.Validate(_defaultCorrectArticle);

            // ASSERT
            Assert.AreSame(_defaultCorrectArticle, article);
            Assert.IsTrue(_defaultCorrectArticle.Description.Length >= Article.DESCRIPTION_MIN_LENGTH
                && _defaultCorrectArticle.Description.Length <= Article.DESCRIPTION_MAX_LENGTH);
        }

        [TestMethod]
        public void Validate_ContentTooShort_Test()
        {
            // ARANGE
            string initialContent = new string('a', Article.CONTENT_MIN_LENGTH - 1);
            _defaultCorrectArticle.Content = initialContent;

            // ACT
            Action action = () => _articleHelper.Validate(_defaultCorrectArticle);

            // ASSERT
            Assert.ThrowsException<ValidationException>(action);
            Assert.IsTrue(_defaultCorrectArticle.Content.Length < Article.CONTENT_MIN_LENGTH);
        }

        [TestMethod]
        public void Validate_ContentTooLong_Test()
        {
            // ARANGE
            string initialContent = new string('a', Article.CONTENT_MAX_LENGTH + 1);
            _defaultCorrectArticle.Content = initialContent;

            // ACT
            Action action = () => _articleHelper.Validate(_defaultCorrectArticle);

            // ASSERT
            Assert.ThrowsException<ValidationException>(action);
            Assert.IsTrue(_defaultCorrectArticle.Content.Length > Article.CONTENT_MAX_LENGTH);
        }

        [TestMethod]
        public void Validate_ContentCorrect_Test()
        {
            // ARANGE
            string initialContent = new string('a', Article.CONTENT_MIN_LENGTH + 1);
            _defaultCorrectArticle.Content = initialContent;

            // ACT
            Article article = _articleHelper.Validate(_defaultCorrectArticle);

            // ASSERT
            Assert.AreSame(_defaultCorrectArticle, article);
            Assert.IsTrue(_defaultCorrectArticle.Content.Length >= Article.CONTENT_MIN_LENGTH
                && _defaultCorrectArticle.Content.Length <= Article.CONTENT_MAX_LENGTH);
        }
    }
}
