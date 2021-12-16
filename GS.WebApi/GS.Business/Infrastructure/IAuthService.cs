using Google.Apis.Auth;
using GS.Domain.Models.User;
using System.Threading.Tasks;

namespace GS.Business.Infrastructure
{
    public interface IAuthService
    {
        string GenerateToken(UserModel user);
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth);
        Task<UserModel> GetUserByLoginAsync(string loginProvider, string providerKey);
        Task AddUserAsync(UserModel user);
        Task AddUserLoginAsync(UserLoginModel userLogin);
    }
}
