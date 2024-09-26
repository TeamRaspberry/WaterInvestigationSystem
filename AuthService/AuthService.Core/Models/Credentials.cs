namespace AuthService.Core.Models
{
    public class Credentials
    {
        public Guid CredentialsKey { get; }
        public string Name { get; }
        public string Surname { get; }
        public DateTime BirthDate { get; }

        private List<User> AttachedUsers = new List<User>();

        public Credentials(string Name, string Surname, DateTime BirthDate, List<User> AttachedUsers)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.BirthDate = BirthDate;
            this.AttachedUsers.AddRange(AttachedUsers);
        }

        public Credentials(Guid CredentialsKey, string Name, string Surname, 
            DateTime BirthDate, List<User> AttachedUsers) : this(Name, Surname, BirthDate, AttachedUsers)
        {
            this.CredentialsKey = CredentialsKey;
        }

        public bool AttachUser(User user)
        {
            if (AttachedUsers.Contains(user))
            {
                return false;
            }

            AttachedUsers.Add(user);

            return true;
        }

        public bool DetachUser(User user)
        {
            if (!AttachedUsers.Contains(user))
            {
                return false;
            }

            AttachedUsers.Remove(user);

            return true;
        }

        public IEnumerable<User> GetAttachedUsers()
        {
            foreach(User user in AttachedUsers)
            {
                yield return user;
            }
        }
    }
}
