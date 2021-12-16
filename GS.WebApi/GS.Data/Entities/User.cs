using System;
using System.Collections.Generic;

namespace GS.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public ICollection<UserLogin> UserLogins { get; set; }
    }
}
