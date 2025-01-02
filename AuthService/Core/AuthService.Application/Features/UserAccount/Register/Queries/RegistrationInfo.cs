namespace AuthService.Application.Features.UserAccount.Register.Queries
{
    public record class RegistrationInfo(string Username, string Password, string Name, string Surname, string Email);
}
