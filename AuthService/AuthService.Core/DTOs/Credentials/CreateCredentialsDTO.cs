namespace AuthService.Core.DTOs.Credentials
{
    public class CreateCredentialsDTO
    {
        public required string name { get; set; }
        public required string surname { get; set; }
        public DateTime birthDate { get; set; } = DateTime.UtcNow;
        public object[] attachedUsers { get; set; } = default!;
    }
}
