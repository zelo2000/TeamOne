using GS.Data.Entities;
using System;

namespace GS.Data.Repositories.UserRead
{
    public interface IUserReadRepository
    {
        User GetUserById(Guid userId);
    }
}
