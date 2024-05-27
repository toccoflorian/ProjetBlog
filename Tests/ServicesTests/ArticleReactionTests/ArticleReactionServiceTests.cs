using DTO.ArticleReactionDTO;
using IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Tests.Mocks.Repository;
using Tests.Mocks.Service;

namespace Services.Tests
{
    [TestClass()]
    public class ArticleReactionServiceTests
    {
        private IArticleReactionService _articleReactionService;
        private ServiceProvider _serviceProvider;
        private ProjetBlogContext _inMemoryContext;

        private readonly string _connectedUserId = "Connected user id";
        private Article _article;
        private ToggleArticleReactionDTO _reactionDTO;

        [TestInitialize]
        public async Task TestInitialize()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<ProjetBlogContext>(option => option.UseInMemoryDatabase("TestDatabase"));

            _serviceProvider = services.BuildServiceProvider();

            _inMemoryContext = _serviceProvider.GetService<ProjetBlogContext>()!;

            _articleReactionService = new ArticleReactionService(
                new LikeServiceMock(_inMemoryContext),
                new ArticleReactionRepositoryMock(_inMemoryContext),
                new DisLikeServiceMock(_inMemoryContext),
                new SupportServiceMock(_inMemoryContext));

            _article = new Article
            {
                Id = 1,
                CategoryId = 1,
                Content = "test",
                Description = "test",
                EditionDate = DateTime.Now,
                Title = "test",
                UserId = "test",
                ImageURL = "test",
            };

            _reactionDTO = new ToggleArticleReactionDTO(_article.Id, _connectedUserId);
            await _inMemoryContext.Articles.AddAsync(_article);
            await _inMemoryContext.SaveChangesAsync();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            _inMemoryContext.Articles.Remove(_article);
            await _inMemoryContext.SaveChangesAsync();
            _serviceProvider.Dispose();
        }

        private async Task<ArticleReaction?> GetArticleReactionAsync() =>
            await _inMemoryContext.ArticleReactions.FindAsync(_connectedUserId + _article.Id);

        private async Task<Like?> GetLikeAsync(int? likeId) => 
            await _inMemoryContext.Likes.FindAsync(likeId);
    
        private async Task<DisLike?> GetDisLikeAsync(int? dislikeId) => 
            await _inMemoryContext.DisLikes.FindAsync(dislikeId);

        private async Task<Support?> GetSupportAsync(int? supportId) =>
            await _inMemoryContext.Supports.FindAsync(supportId);




        [TestMethod()]

        public async Task ToggleLikeArticleAsync_Like_Test()
        {
            // ARANGE:
            // Vérification qu'il n'existe pas de réaction avant le test
            ArticleReaction? ArangeArticleReaction = await GetArticleReactionAsync();

            // ACT:
            // ToggleLike d'un article non liké 
            await _articleReactionService.ToggleLikeArticleAsync(_reactionDTO);

            // ASSERT:
            // La réaction à l'article dois avoir été créée après avoir liké
            // La réaction à l'article créée dois réfèrencer un Like
            // Le Like réfèrencé dois avoir été créer
            ArticleReaction? articleReaction = await GetArticleReactionAsync();
            Like? like = await GetLikeAsync((int)articleReaction?.LikeId!);

            Assert.IsNull(ArangeArticleReaction, 
                "ArticleReaction dois être null avant d'avoir liker.");
            Assert.IsNotNull(articleReaction, 
                "ArticleReaction dois être créer avec pour id 'User.Id + Article.Id' après avoir liker.");
            Assert.IsNotNull(like, 
                "Like dois être créer et réfèrencé par ArticleReaction après avoir liker.");
        }





        [TestMethod]

        public async Task ToggleLikeArticleAsync_UnLike_Test()
        {
            // ARRANGE:
            // Une réaction à l'article dois être créer avant le test
            // La réaction dois réfèrencer un Like avant le test
            // Le Like réfèrencé dois exister avant le test
            await _articleReactionService.ToggleLikeArticleAsync(_reactionDTO);
            ArticleReaction? arangeArticleReaction = await GetArticleReactionAsync();
            Like? arangeLike = await GetLikeAsync(arangeArticleReaction?.LikeId);

            // ACT:
            // ToggleLike d'un article déjà liké
            await _articleReactionService.ToggleLikeArticleAsync(_reactionDTO);

            // ASSERT:
            // La réaction à l'article ne dois plus exister après le test
            // Le Like réfèrencé par la réaction à l'article initiale ne dois plus existé après le test
            Assert.IsNotNull(arangeArticleReaction, 
                "ArticleReaction dois existé avant de UnLiker.");
            Assert.IsNotNull(arangeLike, 
                "Like dois existé et dois être réfèrencé par ArticleReaction avant de UnLiké.");
            Assert.IsNull(await GetArticleReactionAsync(), 
                "ArticleReaction ne dois plus existé après avoir UnLiké.");
            Assert.IsNull(await GetLikeAsync(arangeLike.Id), 
                "Like ne dois plus existé après avoir UnLiker.");
        }




        [TestMethod]

        public async Task ToggleLikeArticleAsync_LikeWhenIsDisLiked_Test()
        {
            // ARANGE:
            // Une réaction à l'article dois exister avant le test 
            // La réaction à l'article dois réfèrencer un DisLike avant le test
            // Le DisLike réfèrencé dois exité avant le test
            await _articleReactionService.ToggleDisLikeArticleAsync(_reactionDTO);
            ArticleReaction? arangeArticleReaction = await GetArticleReactionAsync();
            DisLike? arangeDislike = await GetDisLikeAsync((int)arangeArticleReaction?.DisLikeId!);

            // ACT:
            // ToggleLike d'un article DisLiké
            await _articleReactionService.ToggleLikeArticleAsync(_reactionDTO);

            // ASSERT
            // Le DisLike ne dois plus exister après le test 
            // Une nouvelle réaction dois exister après le test
            // La nouvelle réaction ne dois pas réfèrencer de DisLike après le test
            // La nouvelle réaction dois réfèrencer un Like après le test
            // Le Like réfèrencé dois exister après le test
            ArticleReaction? assertReaction = await GetArticleReactionAsync();

            Assert.IsNotNull(arangeArticleReaction, 
                "ArtcleReaction dois existé avant de passer de DisLiké à Liké.");
            Assert.IsNotNull(arangeDislike, 
                "DisLike dois existé avant de passer de DisLiké à Liké.");
            Assert.IsNull((await GetArticleReactionAsync())?.DisLikeId, 
                "ArticleReaction.DisLikeId dois être null après avoir switché de DiLiké à Liké.");
            Assert.IsNull(await GetDisLikeAsync(assertReaction?.DisLikeId ?? 0), 
                "DisLike dois être null après avoir switché de DisLiké à Liké.");
            Assert.IsNotNull(assertReaction?.LikeId, 
                "ArticleReaction.LikeId dois être précisé après avoir switché de DisLiké à Liké.");
            Assert.IsNotNull(await GetLikeAsync((int)assertReaction.LikeId), 
                "Like dois existé après avoir switché de DisLiké à Liké.");
        }




        [TestMethod]

        public async Task ToggleLikeArticleAsync_LikeWhenIsSupported_Test()
        {
            // ARANGE:
            // Une réaction à l'article dois exister avant le test 
            // La réaction à l'article dois réfèrencer un Support avant le test
            // Le Support réfèrencé dois exité avant le test
            await _articleReactionService.ToggleSupportArticleAsync(_reactionDTO);
            ArticleReaction? arangeArticleReaction = await GetArticleReactionAsync();
            Support? arangeSupport = await GetSupportAsync((int)arangeArticleReaction?.SupportId!);

            // ACT:
            // ToggleLike d'un article Supporté
            await _articleReactionService.ToggleLikeArticleAsync(_reactionDTO);

            // ASSERT
            // Le Support ne dois plus exister après le test 
            // Une nouvelle réaction dois exister après le test
            // La nouvelle réaction ne dois pas réfèrencer de Support après le test
            // La nouvelle réaction dois réfèrencer un Like après le test
            // Le Like réfèrencé dois exister après le test
            ArticleReaction? assertReaction = await GetArticleReactionAsync();

            Assert.IsNotNull(arangeArticleReaction, 
                "ArtcleReaction dois existé avant de passer de Supporté à Liké.");
            Assert.IsNotNull(arangeSupport, 
                "Support dois existé avant de passer de Supporté à Liké.");
            Assert.IsNull((await GetArticleReactionAsync())?.DisLikeId, 
                "ArticleReaction.SupportId dois être null après avoir switché de Supporté à Liké.");
            Assert.IsNull(await GetDisLikeAsync(assertReaction?.DisLikeId ?? 0), 
                "Support dois être null après avoir switché de Supporté à Liké.");
            Assert.IsNotNull(assertReaction?.LikeId, 
                "ArticleReaction.LikeId dois être précisé après avoir switché de Supporté à Liké.");
            Assert.IsNotNull(await GetLikeAsync((int)assertReaction.LikeId), 
                "Like dois existé après avoir switché de Supporté à Liké.");
        }

    }
}

