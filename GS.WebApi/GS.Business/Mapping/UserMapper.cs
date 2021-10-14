﻿namespace GS.Business.Mapping
{
    public static class UserMapper
    {
        public static Domain.Models.User ToDomain(this Data.Entities.User user)
        {
            return new Domain.Models.User
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            };
        }
    }
}
