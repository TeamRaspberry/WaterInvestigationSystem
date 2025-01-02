using AuthService.Application.ResultBase;

namespace AuthService.Application.Features.ExternalApplication.Register
{
    public interface IApplicationRegister
    {
        //Returns client Id and client secret
        public Task<Result<(Guid, string)>> RegisterApplication(string name);
    }
}
