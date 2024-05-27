using IRepositories;
using IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace Repositories.Tests
{
    [TestClass]
    public class ArticleRepositoryTests
    {
#pragma warning disable CS8618
        private IArticleRepository _articleRepository;
        private ServiceProvider _serviceProvider;
        private ProjetBlogContext _inMemoryContext;
        private Article _defaultArticle;
        private ArticleRead _defaultArticleRead;
#pragma warning restore CS8618


        [TestInitialize]
        public void TestInitialize()
        {
            ServiceCollection? services = new ServiceCollection();

            // Register your services here
            services.AddDbContext<ProjetBlogContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));

            // Build the service provider
            _serviceProvider = services.BuildServiceProvider();

            // Get the services
            _inMemoryContext = _serviceProvider.GetService<ProjetBlogContext>()!;
            _articleRepository = new ArticleRepository(_inMemoryContext);
            this._defaultArticle = new Article
            {
                Id = 1,
                CategoryId = 1,
                Title = "TitleTest",
                Description = "DescriptionTest",
                Content = "ContentTest",
                EditionDate = DateTime.Now,
                UserId = "connected user id",
                ImageURL = "ImageURLTest",
            };
            this._defaultArticleRead = new ArticleRead
            {
                Id = this._defaultArticle.UserId + _defaultArticle.Id,
                ArticleId = _defaultArticle.Id,
                UserId = _defaultArticle.UserId,
                Date = DateTime.Now,
            };
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _serviceProvider.Dispose();
        }


        /// <summary>
        /// Check cascading deletation of 'ArticleRead' when 'Article' is deleted
        /// </summary>
        /// <returns>True in success</returns>
        [TestMethod]
        public async Task DeleteAsync_CheckCascadingDeletationOfArticleReadWithArticleDeletation_Test()
        {
            // Arrange
            await _inMemoryContext.Articles.AddAsync(_defaultArticle);
            await _inMemoryContext.ArticleReads.AddAsync(_defaultArticleRead);

            Article? initialArticle = await _inMemoryContext.Articles.FindAsync(_defaultArticle.Id);
            ArticleRead? initialArticleRead = await _inMemoryContext.ArticleReads.FindAsync(_defaultArticleRead.Id);

            // Act
            await _articleRepository.DeleteAsync(_defaultArticle);

            // Assert 
            Article? resultArticle = await _inMemoryContext.Articles.FindAsync(_defaultArticle.Id);
            ArticleRead? resultArticleRead = await _inMemoryContext.ArticleReads.FindAsync(_defaultArticleRead.Id);

            Assert.IsNotNull(initialArticle, "L'Article n'as pas été créer avant le test.");
            Assert.IsNotNull(initialArticleRead, "L'ArticleRead n'as pas été créer avant le test.");
            Assert.IsNull(resultArticle, "L'Article n'as pas été supprimmer pendant le test.");
            Assert.IsNull(resultArticleRead, "L'ArticleRead n'as pas été supprimmer en cascade pendant le test.");
        }
    }
}
