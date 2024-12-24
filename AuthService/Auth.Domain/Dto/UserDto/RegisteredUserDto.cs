namespace Auth.Domain.Dto.UserDto
{
    public class RegisteredUserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
