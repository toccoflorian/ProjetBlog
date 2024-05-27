
using IRepositories;
using IServices;
using Models;

namespace Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        public LikeService(ILikeRepository likeRepository)
        {
            this._likeRepository = likeRepository;
        }

        public async Task<int> LikeAsync()
        {
            Like like = new Like();
            await this._likeRepository.CreateAsync(like);
            await this._likeRepository.SaveChangesAsync();
            return like.Id;
        }

        public async Task<Like> GetByIdAsync(int likeId)
        {
            Like? like = await this._likeRepository.GetByIdAsync(likeId);
            if(like == null)
            {
                throw new Exception("Like non-referencé");
            }
            return like;
        }

        public async Task UnLikeAsync(int likeId)
        {
            this._likeRepository.Delete(await this.GetByIdAsync(likeId));
             await this._likeRepository.SaveChangesAsync();
        }
    }
}
