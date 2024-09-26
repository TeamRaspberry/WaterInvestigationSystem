namespace AuthService.Persistance.Entities
{
    public class CredentialsEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public List<UserEntity> AttachedUsers { get; set; } = new List<UserEntity>();
    }
}
