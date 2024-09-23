namespace AuthService.Core.DTOs.User
{
    public class CreateUserDTO
    {
        public required string username { get; set; }
        public required string password { get; set; }
        public required string email { get; set; }
        public object? roleId { get; set; }
    }
}
