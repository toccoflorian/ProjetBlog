using IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ProjetBlogContext _context;
        public AuthRepository(ProjetBlogContext context)
        {
            this._context = context;
        }

        public async Task<int> CreateAsync(User user)
        {
            await this._context.Users.AddAsync(user);
            return await this._context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
