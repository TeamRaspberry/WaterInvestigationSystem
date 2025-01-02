using AuthService.Core.Models;

namespace AuthService.Core.Abstractions.Repositories
{
    public interface IApplicationRepository
    {
        public Task<bool> IsExists(Guid id, string token);
        public Task<IEnumerable<ExternalApplicationModel>> GetAllAsync();
        public Task CreateAsync(ExternalApplicationModel application);
    }
}
