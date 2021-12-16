using System;

namespace GS.Domain.Models.User
{
    public class UserLoginModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        public string ProviderDisplayName { get; set; }

        public Guid UserId { get; set; }
    }
}
