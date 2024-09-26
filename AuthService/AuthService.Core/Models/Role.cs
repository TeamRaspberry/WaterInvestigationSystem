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
            this.RoleKey = Guid.NewGuid();
        }

        public Role(Guid RoleKey, string Name, bool IsDefault, bool IsAdmin) 
            : this(IsDefault, IsAdmin)
        {
            this.RoleKey = RoleKey;
            this.Name = Name;
        }

        public static bool operator ==(Role left, Role right)
        {
            return left.RoleKey == right.RoleKey;
        }

        public static bool operator !=(Role left, Role right)
        {
            return left.RoleKey != right.RoleKey;
        }

        public override bool Equals(object? obj)
        {
            return obj is Role && ((Role)obj).RoleKey == this.RoleKey;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
