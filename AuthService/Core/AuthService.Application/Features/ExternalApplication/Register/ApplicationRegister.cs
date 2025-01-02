using AuthService.Application.Abstractions.Cipher;
using AuthService.Application.Abstractions.Hash;
using AuthService.Application.ResultBase;
using AuthService.Core.Abstractions.Repositories;
using AuthService.Core.Models;
using System.Text;

namespace AuthService.Application.Features.ExternalApplication.Register
{
    public class ApplicationRegister(IApplicationRepository repository, IEncoder encoder, IHasher hasher) : IApplicationRegister
    {
        public async Task<Result<(Guid, string)>> RegisterApplication(string name)
        {
            Guid id = Guid.NewGuid();

            string token = GenerateToken(id, name);

            string encodedToken = encoder.Encode(token);

            ExternalApplicationModel application = new(id, name, encodedToken);

            await repository.CreateAsync(application);

            return Result<(Guid, string)>.Success((id, encodedToken));
        }

        private string GenerateToken(Guid id, string name)
        {

            StringBuilder builder = new StringBuilder();

            builder.Append(id);
            builder.Append('_');
            builder.Append(name);

            return hasher.Hash(builder.ToString());
        }
    }
}
