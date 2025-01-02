using System.Net.Mail;

namespace AuthService.Core.Models
{
    public sealed class User
    {
        public Guid Id { get; private init; } = Guid.NewGuid();
        public string Name { get; private set; } = null!;
        public string Surname { get; private set; } = null!;
        public string Username { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public string Email { get; private set; } = null!;

        public User(string username, string password, string name, string surname, string email)
        {
            Username = !string.IsNullOrWhiteSpace(username)
                ? username
                : throw new ArgumentException("Username can't be empty", paramName: nameof(username));

            Password = !string.IsNullOrWhiteSpace(password)
                ? password
                : throw new ArgumentException("Password can't be empty", paramName: nameof(password));

            Name = !string.IsNullOrWhiteSpace(name) 
                ? name
                : throw new ArgumentException("Name can't be empty", paramName: nameof(name));

            Surname = !string.IsNullOrWhiteSpace(surname)
                ? name
                : throw new ArgumentException("Surname can't be empty", paramName: nameof(surname));

            ValidateEmail(email);

            Email = email;
        }

        public User(Guid id, string username, string password, string name, string surname, string email) : this(username, password, name, surname, email)
        {
            Id = id;
        }

        public void ChangeEmail(string email) 
        {
            ValidateEmail(email);

            Email = email;
        }

        public void ChangePassword(string password)
        {
            Password = !string.IsNullOrWhiteSpace(password)
                ? password
                : throw new ArgumentException("Password can't be empty", paramName: nameof(password));
        }

        private void ValidateEmail(string email)
        {
            bool isEmail = MailAddress.TryCreate(email, out _);

            if (!isEmail)
            {
                throw new ArgumentException("Email is in invalid format", paramName: nameof(email));
            }
        }

    }
}
