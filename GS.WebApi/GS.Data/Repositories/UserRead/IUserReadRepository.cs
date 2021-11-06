using GS.Data.Entities;
using System;
using System.Threading.Tasks;

namespace GS.Data.Repositories.UserRead
{
    public interface IUserReadRepository
    {
        Task<User> GetUserById(Guid userId);

        Task<User> GetByEmailAndPasswordHashAsync(string email, string passwordHash);
    }
}
