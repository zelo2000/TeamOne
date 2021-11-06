using GS.Domain.Models.User;
using System.Threading.Tasks;

namespace GS.Business.Infrastructure
{
    public interface IAuthService
    {
        Task<LogInResultModel> LogIn(LogInModel model);
    }
}
