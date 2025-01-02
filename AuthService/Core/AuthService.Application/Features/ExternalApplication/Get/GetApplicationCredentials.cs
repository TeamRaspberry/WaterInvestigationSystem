using AuthService.Application.Abstractions.Cipher;
using AuthService.Application.Features.ExternalApplication.Get.Responses;
using AuthService.Core.Abstractions.Repositories;
using AuthService.Core.Models;

namespace AuthService.Application.Features.ExternalApplication.Get
{
    public class GetApplicationCredentials(IApplicationRepository repository, IEncoder encoder) : IGetApplicationCredentials
    {
        public async Task<IEnumerable<ApplicationCredentials>> GetAll()
        {
            IEnumerable<ExternalApplicationModel> applications = await repository.GetAllAsync();

            return applications.Select(MapCredentials);
        }

        public async Task<bool> IsValid(Guid id, string secret)
        {
            return await repository.IsExists(id, secret);
        }

        private ApplicationCredentials MapCredentials(ExternalApplicationModel model)
        {
            string decodedToken = encoder.Decode(model.Token);

            return new ApplicationCredentials(
                model.Id,
                model.Name,
                decodedToken);
        }
    }
}
