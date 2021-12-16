using GS.Data.Entities;
using GS.Domain.Models.User;

namespace GS.Business.Mapping
{
    public static class UserLoginMapper
    {
        public static UserLogin ToEntity(this UserLoginModel userLogin)
        {
            return new UserLogin
            {
                ProviderDisplayName = userLogin.ProviderDisplayName,
                LoginProvider = userLogin.LoginProvider,
                ProviderKey = userLogin.ProviderKey,
                UserId = userLogin.UserId
            };
        }
    }
}
