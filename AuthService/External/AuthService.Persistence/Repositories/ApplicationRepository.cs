using AuthService.Core.Abstractions.Repositories;
using AuthService.Core.Models;
using AuthService.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Repositories
{
    public class ApplicationRepository(DatabaseContext context) : IApplicationRepository
    {
        public async Task CreateAsync(ExternalApplicationModel application)
        {
            await context.Applications.AddAsync(application);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExternalApplicationModel>> GetAllAsync()
        {
            return await Task.FromResult(context.Applications.AsNoTracking());
        }

        public async Task<bool> IsExists(Guid id, string token)
        {
            return context
                .Applications
                .AsNoTracking()
                .Any(x => x.Id == id && x.Token == token);
        }
    }
}
