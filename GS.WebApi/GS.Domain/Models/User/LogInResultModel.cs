using System;

namespace GS.Domain.Models.User
{
    public class LogInResultModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public Guid UserId { get; set; }
    }
}
