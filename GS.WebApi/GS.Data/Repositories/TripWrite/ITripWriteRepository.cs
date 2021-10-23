using GS.Data.Entities;
using System;
using System.Threading.Tasks;

namespace GS.Data.Repositories.TripWrite
{
    public interface ITripWriteRepository
    {
        Task CreateTrip(Trip trip);

        Task AddToDoNode(Guid tripId, ToDoNode node);
    }
}
