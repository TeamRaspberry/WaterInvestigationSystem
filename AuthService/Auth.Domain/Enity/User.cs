using Auth.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Enity
{
    public class User : IEntityId<long>
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public UserToken UserToken { get; set; }
     
        public List<Role> Role { get; set; }

    }
}
