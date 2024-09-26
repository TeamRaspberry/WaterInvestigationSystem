namespace AuthService.Persistance.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime SecurityStamp { get; set; }
        public RoleEntity Role { get; set; } = default!;
    }
}
