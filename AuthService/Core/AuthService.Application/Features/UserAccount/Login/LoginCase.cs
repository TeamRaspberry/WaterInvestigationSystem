using AuthService.Application.Abstractions.Hash;
using AuthService.Application.Errors;
using AuthService.Application.Features.UserAccount.Login.Mappers;
using AuthService.Application.Features.UserAccount.Login.Responses;
using AuthService.Application.ResultBase;
using AuthService.Core.Abstractions.Repositories;
using AuthService.Core.Models;

namespace AuthService.Application.Features.UserAccount.Login
{
    public class LoginCase(IUserRepository repository, IHasher hasher) : ILoginCase
    {
        public async Task<Result<UserInfo>> GetInfoAsync(string username)
        {
            User? savedUser = await repository.GetOneAsync(username);

            if (savedUser == null)
            {
                return Result<UserInfo>.Failure(UserErrors.NotFound);
            }

            UserInfo info = UserMapper.MapInfo(savedUser);

            return Result<UserInfo>.Success(info);
        }

        public async Task<Result> LoginAsync(string username, string password)
        {
            User? savedUser = await repository.GetOneAsync(username);

            if (savedUser == null)
            {
                return Result.Failure(UserErrors.NotFound);
            }

            string hashedPassword = hasher.Hash(password);

            if (savedUser.Password != hashedPassword)
            {
                return Result.Failure(UserErrors.InvalidPassword);
            }

            return Result.Success();
        }
    }
}
