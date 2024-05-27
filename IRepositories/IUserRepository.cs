using Models;

namespace IRepositories
{
    public interface IUserRepository
    {
        public Task Update();
        public Task<User> GetById();
        public Task<List<User>> getAll();
    }
}
