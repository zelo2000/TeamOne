using GS.Data.Entities;
using System.Threading.Tasks;

namespace GS.Data.Repositories.UserRead
{
    public interface IUserReadRepository
    {
        Task<User> GetUserByLoginAsync(string loginProvider, string providerKey);
    }
}
