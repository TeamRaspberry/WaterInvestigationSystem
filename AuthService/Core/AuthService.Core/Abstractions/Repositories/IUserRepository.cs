using AuthService.Core.Models;

namespace AuthService.Core.Abstractions.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetOneAsync(string username);
        public Task CreateAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(Guid id);
        public Task SaveAsync();
    }
}
