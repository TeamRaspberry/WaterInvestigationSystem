namespace AuthService.Core.Models
{
    public class Role
    {
        public Guid RoleKey { get; } 
        private string _Name = default!;
        public string Name 
        { 
            get => _Name; 
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                _Name = value;
            } 
        }
        public bool IsDefault { get; }
        public bool IsAdmin { get; }

        private Role(bool IsDefault, bool IsAdmin)
        {
            this.IsDefault = IsDefault;
            this.IsAdmin = IsAdmin;
        }

        public Role(string Name, bool IsDefault, bool IsAdmin)
            : this(IsDefault, IsAdmin)
        {
            this.Name = Name;
        }

        public Role(Guid RoleKey, string Name, bool IsDefault, bool IsAdmin) 
            : this(Name, IsDefault, IsAdmin)
        {
            this.RoleKey = RoleKey;
        }
    }
}
