using Models;

namespace IRepositories
{
    public interface ISupportRepository
    {
        public Task CreateAsync(Support support);
        public void Delete(Support support);
        public Task<Support?> GetByIdAsync(int supportId);
        public Task<int> SaveChangesAsync();
    }
}
