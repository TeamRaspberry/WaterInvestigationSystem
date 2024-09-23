namespace AuthService.Core.DTOs.Credentials
{
    public class GetCredentialsDTO
    {
        public string name { get; set; } = default!;
        public string surname { get; set; } = default!;
        public DateTime birthDate { get; set; }
    }
}
