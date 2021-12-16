using GS.Data.Entities;
using GS.Domain.Models.User;

namespace GS.Business.Mapping
{
    public static class UserMapper
    {
        public static UserModel ToDomain(this User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            };
        }

        public static User ToEntity(this UserModel user)
        {
            return new User
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            };
        }
    }
}
