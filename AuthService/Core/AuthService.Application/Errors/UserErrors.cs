using AuthService.Application.ResultBase;

namespace AuthService.Application.Errors
{
    internal static class UserErrors
    {
        public static BaseError NotFound = new(404, "Not found");
        public static BaseError InvalidPassword = new(400, "Wrong password");
        public static BaseError DuplicateUser = new(400, "User already exists");
    }
}
