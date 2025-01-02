namespace AuthService.Application.Features.UserAccount.Login.Responses
{
    public record class UserInfo(Guid Id, string Username, string Name, string Surname, string Email);
}
