using AuthService.Application.Abstractions.Hash;
using AuthService.Application.Errors;
using AuthService.Application.Features.UserAccount.Register.Queries;
using AuthService.Application.ResultBase;
using AuthService.Core.Abstractions.Repositories;
using AuthService.Core.Exceptions;
using AuthService.Core.Models;

namespace AuthService.Application.Features.UserAccount.Register
{
    public class RegisterCase(IUserRepository repository, IHasher hasher) : IRegisterCase
    {
        public async Task<Result<Guid>> Register(RegistrationInfo info)
        {
            (string username, string password, string name, string surname, string email) = info;

            string hashedPassword = hasher.Hash(password);

            User newUser = new(username, hashedPassword, name, surname, email);

            try
            {
                await repository.CreateAsync(newUser);
                await repository.SaveAsync();
                return Result<Guid>.Success(newUser.Id);
            }
            catch (DuplicateUserExcpetion)
            {
                return Result<Guid>.Failure(UserErrors.DuplicateUser);
            }
        }
    }
}
