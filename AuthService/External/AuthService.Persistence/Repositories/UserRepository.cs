using AuthService.Core.Abstractions.Repositories;
using AuthService.Core.Exceptions;
using AuthService.Core.Models;
using AuthService.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Repositories
{
    public class UserRepository(DatabaseContext context) : IUserRepository
    {
        public async Task CreateAsync(User user)
        {
            bool isExists = await context.Users.AsNoTracking().AnyAsync(x => x.Id == user.Id || x.Username == user.Username);

            if (isExists) 
            {
                throw new DuplicateUserExcpetion();
            }

            await context.Users.AddAsync(user);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetOneAsync(string username)
        {
            User? user = await context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Username == username);

            return user;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
