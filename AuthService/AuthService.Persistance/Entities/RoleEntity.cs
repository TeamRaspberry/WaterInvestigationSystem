namespace AuthService.Persistance.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDefault { get; set; }
        public bool IsAdmin { get; set; }
    }
}
