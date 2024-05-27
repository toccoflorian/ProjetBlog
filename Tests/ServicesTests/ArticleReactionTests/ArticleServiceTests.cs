using CONST;
using DTO.ArticleDTO;
using IRepositories;
using IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Tests.Mocks.Helpers;
using Tests.Mocks.Repository;
using Tests.Mocks.Service;

namespace Services.Tests
{
    [TestClass()]
    public class ArticleServiceTests
    {
        private ArticleService _articleService;
        private ServiceProvider _serviceProvider;
        private ProjetBlogContext _inMemoryContext;
        private Article _defaultArticle;
        private string _connectedUserId = "connected user id";
        private WriteArticleServiceDTO _defaultWriteArticleDTO;

        [TestInitialize]
        public async Task TestInitialize()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<ProjetBlogContext>( options => 
                options.UseInMemoryDatabase("TestDatabase"));
            //services.AddScoped<IArticleService, ArticleService>();
            //services.AddScoped<IArticleRepository, ArticleRepositoryMock>();
            //services.AddScoped<IArticleReadService, ArticleReadServiceMock>();

            _serviceProvider = services.BuildServiceProvider();
            _inMemoryContext = _serviceProvider.GetService<ProjetBlogContext>();
            _articleService = new ArticleService(new ArticleRepositoryMock(_inMemoryContext), new ArticleReadServiceMock(), new ArticleHelperMock());

            _defaultArticle = new Article
            {
                Id = 1,
                CategoryId = 1,
                Title = "TitleTest",
                Description = "DescriptionTest",
                Content = "ContentTest",
                EditionDate = DateTime.Now,
                ImageURL = null,
                UserId = _connectedUserId,
                UpdatedDate = null,
            };
            _defaultWriteArticleDTO = new WriteArticleServiceDTO(new WriteArticleRequestDTO
            {
                Title = _defaultArticle.Title,
                Description = _defaultArticle.Description,
                Content = _defaultArticle.Content,
                CategoryId = _defaultArticle.CategoryId,
                ImageURL = _defaultArticle.ImageURL
            }, _connectedUserId);
        }

        [TestCleanup]
        public async Task TestClenup()
        {
            Article? article = await GetArticleAsync();
            if (article != null)
            { 
                _inMemoryContext.Articles.Remove(article);
                await _inMemoryContext.SaveChangesAsync();
            }
            _serviceProvider.Dispose();
        }

        private async Task<Article?> GetArticleAsync() => 
            await _inMemoryContext.Articles.FindAsync(_defaultArticle.Id);

        // CheckArticlePersistency
        // WriteArticleWhithDefaultImageURL
        // WriteArticleWithCustomImageURL
        [TestMethod()]
        public async Task WriteAsync_CheckPropertiesAssignmentAndArticlePersistency_Test()
        {
            // ARANGE:
            // L'article ne dois pas exiter en BD avant l'ACT
            Article? InitialArticle = await GetArticleAsync();

            // ACT:
            // Ecrire un article
            await _articleService.WriteAsync(_defaultWriteArticleDTO);

            // ASSERT:
            Article? resultArticle = await GetArticleAsync();

            Assert.IsNull(InitialArticle,
                "L'article ne dois pas exister en BD avant l'ACT");
            Assert.IsNotNull(resultArticle,
                "L'article écrit dois exister en BD après l'ACT");
            Assert.AreEqual(resultArticle.Title, _defaultWriteArticleDTO.Title,
                "Title de l'article écrit dois avoir la même valeur que InitialTitle");
            Assert.AreEqual(resultArticle.Description, _defaultWriteArticleDTO.Description,
                "Description de l'article écrit n'as pas la même valeur que InitialDescription");
            Assert.AreEqual(resultArticle.Content, _defaultWriteArticleDTO.Content,
                "Content de l'article écrit dois avoir la même valeur que InitialContent");
            Assert.AreEqual(resultArticle.CategoryId, _defaultWriteArticleDTO.CategoryId,
                "CategoryId de l'article écrit dois avoir la même valeur que InitialCategoryId");
            Assert.AreEqual(resultArticle.UserId, _connectedUserId,
                "UserId de l'article écrit dois avoir la même valeur que ConnectedUser");
            Assert.IsInstanceOfType<DateTime>(resultArticle.EditionDate, nameof(DateTime),
                "EditionDate de l'article écrit dois avoir une valeur de type DateTime");
            Assert.IsNull(resultArticle.UpdatedDate,
                "UpdatedDate de l'article écrit dois avoir une valeur null");
        }

        [TestMethod]
        public async Task WriteArticle_WriteArticleWhithDefaultImageURL_Test()
        {
            // ASSERT
            // L'URL d'image d'article par defaut dois être assignée lorsque ImageURL n'est pas renseigné
            // à l'écriture d'un article
            string expectedImageURL = CONST.ARTICLE.DEFAULT_IMAGE_URL;
            // Verification que l'ImageURL renseigné est null
            string? InitialImageURL = _defaultWriteArticleDTO.ImageURL;

            // ACT
            // Ecrire un article sans renseigner ImageURL
            await _articleService.WriteAsync(_defaultWriteArticleDTO);

            // ASSERT
            Article? resultArticle = await GetArticleAsync();
            // Verification que ImageURL n'est pas renseigné dans le DTO pour écrire l'article
            Assert.IsNull(InitialImageURL, 
                "ImageURL n'est pas null avant le test.");
            // ImageUrl de l'article écrit dois être l'url d'image d'article par defaut
            Assert.AreSame(resultArticle?.ImageURL, expectedImageURL,
                "ImageURL ne correspond pas à l'url d'image d'article par defaut.");
        }

        [TestMethod]
        public async Task WriteAsync_WriteArticleWithCustomImageURL_Test()
        {
            // ARANGE
            // L'url donnée par l'utilisateur dois être assignée à l'article écrit
            string expectedImageURL = "https://CustomImageURLTest.com";
            // ImageURL dois être renseigné pour l'écriture de l'article
            _defaultWriteArticleDTO.ImageURL = expectedImageURL;

            // ACT
            // Ecrire un article en renseignant une url personnalisée d'image d'article
            await _articleService.WriteAsync(_defaultWriteArticleDTO);
            
            // ASSERT
            Article? resultArticle = await GetArticleAsync();
            // Verification qu'une url personnalisée d'image d'article est renseignée
            Assert.AreSame(expectedImageURL, _defaultWriteArticleDTO.ImageURL,
                "L'url personnalisée d'image d'article n'as pas été assignée au WriteArticleDTO avant le test.");
            // Verification que l'url personnalisée d'image d'article se retrouve dans l'article écrit
            Assert.AreSame(expectedImageURL, resultArticle?.ImageURL,
                "ImageURL de l'article écrit ne correspond pas l'URL attendue.");
        }


    }
}