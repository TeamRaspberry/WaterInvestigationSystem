using AuthService.Application.Features.UserAccount.Login.Responses;
using AuthService.Application.ResultBase;

namespace AuthService.Application.Features.UserAccount.Login
{
    public interface ILoginCase
    {
        public Task<Result> LoginAsync(string username, string password);
        public Task<Result<UserInfo>> GetInfoAsync(string username);
    }
}
