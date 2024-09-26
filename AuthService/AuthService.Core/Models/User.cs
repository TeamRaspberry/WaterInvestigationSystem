﻿using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AuthService.Core.Models
{
    public class User
    {
        public Guid UserKey { get; }
        private string _UserName = default!;
        public string UserName 
        { 
            get => _UserName;
            set 
            {
                if (string.IsNullOrEmpty(value) ||
                    value.Length <= 5)
                {
                    return;
                }

                _UserName = value;
            }
        }

        private string _Password = default!;
        public string Password
        {
            get => _Password;
            set
            {
                if (string.IsNullOrEmpty(value) ||
                    value.Length <= 8)
                {
                    return;
                }

                _Password = Encoding.Default
                    .GetString(SHA256
                        .Create()
                        .ComputeHash(Encoding.Default
                            .GetBytes(value)));
            }
        }

        private string _Email = default!;
        public string Email
        {
            get => _Email;
            set
            {
                if (string.IsNullOrEmpty(value) ||
                    !Regex.IsMatch(value, "^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov|ru)$"))
                {
                    return;
                }

                _Email = value;
            }
        }

        public Role Role { get; private set; }

        public User(string UserName, string Password, string Email, Role Role)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.Email = Email;
            this.Role = Role;
        }

        public User(Guid UserKey, string UserName, string Password, string Email, Role Role) 
            : this(UserName, Password, Email, Role)
        {
            this.UserKey = UserKey;
        }

        public void ChangeRole(Role role)
        {
            this.Role = role;
        }
    }
}
