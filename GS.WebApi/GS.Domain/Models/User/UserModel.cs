﻿using System;

namespace GS.Domain.Models.User
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }
    }
}
