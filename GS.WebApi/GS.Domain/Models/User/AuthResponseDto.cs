using System;

namespace GS.Domain.Models.User
{
    public class AuthResponseDto
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public Guid Id { get; set; }
    }
}
