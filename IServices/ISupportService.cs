
using Models;

namespace IServices
{
    public interface ISupportService
    {
        public Task<int> SupportAsync();
        public Task UnSupportAsync(int supportId);
        public Task<Support?> GetByIdAsync(int supportId);
    }
}
