namespace AuthService.Application.ResultBase
{
    public record class BaseError(int StatusCode, string Message);
}
