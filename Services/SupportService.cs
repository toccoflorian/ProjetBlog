
using IRepositories;
using IServices;
using Models;

namespace Services
{
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _supportRepository;
        public SupportService(ISupportRepository supportRepository)
        {
            this._supportRepository = supportRepository;
        }

        public async Task<Support> GetByIdAsync(int supportId)
        {
            Support? support = await this._supportRepository.GetByIdAsync(supportId);
            if(support == null)
            {
                throw new Exception("Support non référencé !");
            }
            return support;
        }

        public async Task<int> SupportAsync()
        {
            Support support = new Support();
            await this._supportRepository.CreateAsync(support);
            await this._supportRepository.SaveChangesAsync();
            return support.Id;
        }

        public async Task UnSupportAsync(int supportId)
        {
            this._supportRepository.Delete(await this.GetByIdAsync(supportId));
            await this._supportRepository.SaveChangesAsync();
        }
    }
}
