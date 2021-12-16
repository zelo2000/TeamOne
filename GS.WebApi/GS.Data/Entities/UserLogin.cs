﻿using System;

namespace GS.Data.Entities
{
    public class UserLogin
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        public string ProviderDisplayName { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
