namespace AuthService.Core.DTOs.User
{
    public class UpdateUserDTO
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
    }
}
