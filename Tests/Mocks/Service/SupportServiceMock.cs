
using IServices;
using Models;

namespace Tests.Mocks.Service
{
    internal class SupportServiceMock(ProjetBlogContext inMemoryContext) : ISupportService
    {
        public async Task<Support?> GetByIdAsync(int supportId)
        {
            return await inMemoryContext.Supports.FindAsync(supportId);
        }

        public async Task<int> SupportAsync()
        {
            await inMemoryContext.Supports.AddAsync(new Support());
            return await inMemoryContext.SaveChangesAsync();
        }

        public async Task UnSupportAsync(int supportId)
        {
            Support? support = await GetByIdAsync(supportId);
            if(support != null)
                inMemoryContext.Supports.Remove(support);
            await inMemoryContext.SaveChangesAsync();
        }
    }
}
