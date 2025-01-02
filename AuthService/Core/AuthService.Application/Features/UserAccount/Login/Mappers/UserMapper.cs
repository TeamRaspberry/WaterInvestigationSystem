using AuthService.Application.Features.UserAccount.Login.Responses;
using AuthService.Core.Models;

namespace AuthService.Application.Features.UserAccount.Login.Mappers
{
    public class UserMapper
    {
        public static UserInfo MapInfo(User user)
        {
            return new UserInfo(
                user.Id,
                user.Username,
                user.Name,
                user.Surname,
                user.Email);
        }
    }
}
