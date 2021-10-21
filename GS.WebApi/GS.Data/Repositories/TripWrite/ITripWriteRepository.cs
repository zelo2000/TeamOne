using GS.Data.Entities;
using System.Threading.Tasks;

namespace GS.Data.Repositories.TripWrite
{
    public interface ITripWriteRepository
    {
        Task CreateTrip(Trip trip);
    }
}
