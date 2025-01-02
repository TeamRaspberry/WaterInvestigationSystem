using AuthService.Application.Features.ExternalApplication.Get.Responses;

namespace AuthService.Application.Features.ExternalApplication.Get
{
    public interface IGetApplicationCredentials
    {
        public Task<IEnumerable<ApplicationCredentials>> GetAll();

        public Task<bool> IsValid(Guid id, string secret);
    }
}
