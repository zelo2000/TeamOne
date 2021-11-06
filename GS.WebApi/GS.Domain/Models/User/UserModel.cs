using System;

namespace GS.Domain.Models.User
{
    public class UserModel : UserBaseModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
    }
}
