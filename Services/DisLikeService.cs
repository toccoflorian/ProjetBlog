
using IRepositories;
using IServices;
using Models;

namespace Services
{
    public class DisLikeService : IDisLikeService
    {
        private readonly IDisLikeRepository _disLikeRepository;
        public DisLikeService(IDisLikeRepository disLikeRepository)
        {
            this._disLikeRepository = disLikeRepository;
        }

        public async Task<int> DislikeAsync()
        {
            DisLike disLike = new DisLike(); 
            await this._disLikeRepository.CreateAsync(disLike);
            await this._disLikeRepository.SaveChangesAsync();
            return disLike.Id;
        }

        public async Task<DisLike> GetByIdAsync(int dislikeId)
        {
            DisLike? dislike = await this._disLikeRepository.GetByIdAsync(dislikeId);
            if(dislike == null)
            {
                throw new Exception("DisLike non referencé !");
            }
            return dislike;
        }

        public async Task UnDislikeAsync(int dislikeId)
        {
            this._disLikeRepository.Delete(await this.GetByIdAsync(dislikeId));
            await this._disLikeRepository.SaveChangesAsync();
        }
    }
}
