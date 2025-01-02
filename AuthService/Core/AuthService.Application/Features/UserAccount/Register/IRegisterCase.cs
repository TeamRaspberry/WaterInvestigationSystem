using AuthService.Application.Features.UserAccount.Register.Queries;
using AuthService.Application.ResultBase;

namespace AuthService.Application.Features.UserAccount.Register
{
    public interface IRegisterCase
    {
        public Task<Result<Guid>> Register(RegistrationInfo info);
    }
}
