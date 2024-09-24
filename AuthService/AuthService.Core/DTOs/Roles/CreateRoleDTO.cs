namespace AuthService.Core.DTOs.Roles
{
    public class CreateRoleDTO
    {
        public required string name { get; set; }
        public bool isDefault { get; set; }
        public bool isAdmin { get; set; }
    }
}
