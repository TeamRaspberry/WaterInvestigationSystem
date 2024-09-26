namespace AuthService.Core.DTOs.Roles
{
    public class UpdateRoleStateDTO
    {
        public Guid roleId { get; set; }
        public bool isDefault { get; set; }
        public bool isAdmin { get; set; }
    }
}
