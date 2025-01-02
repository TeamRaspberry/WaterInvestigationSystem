namespace AuthService.Application.Features.ExternalApplication.Get.Responses
{
    public record class ApplicationCredentials(Guid ApplicationId, string ApplicationName, string ApplicationClientSecret);
}
