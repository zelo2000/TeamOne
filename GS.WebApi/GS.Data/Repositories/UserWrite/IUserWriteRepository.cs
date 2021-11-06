using GS.Data.Entities;
using System.Threading.Tasks;

namespace GS.Data.Repositories.UserWrite
{
    public interface IUserWriteRepository
    {
        Task AddUser(User user);
    }
}
