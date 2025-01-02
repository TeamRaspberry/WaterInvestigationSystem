namespace AuthService.Web.ViewModels.Auth
{
    public record class AuthRequest(Guid ClientId, string ClientSecret, string RedirectUrl);
}
