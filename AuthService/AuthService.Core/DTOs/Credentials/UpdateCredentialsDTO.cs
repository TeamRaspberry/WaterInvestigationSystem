namespace AuthService.Core.DTOs.Credentials
{
    public class UpdateCredentialsDTO
    {
        public string? name { get; set; }
        public string? surname { get; set; }
        public DateTime? birthdate { get; set; }
    }
}
