using GS.Data.Entities;
using GS.Domain.Enums;
using GS.Domain.Models.Trip;
using System;
using System.Threading.Tasks;

namespace GS.Data.Repositories.TripWrite
{
    public interface ITripWriteRepository
    {
        Task CreateTrip(Trip trip);

        Task UpdateTrip(Guid tripId, TripBaseModel trip);

        Task DeleteTrip(Guid tripId);

        Task SetTripStatus(Guid tripId, TripStatus status);

        // To Do Node
        Task AddToDoNode(Guid tripId, ToDoNode node);

        Task UpdateToDoNode(Guid nodeId, ToDoNode node);

        Task SetToDoNodeStatus(Guid nodeId, NodeStatus status);

        Task DeleteToDoNode(Guid nodeId);

        // Item To Take
        Task AddItem(Guid tripId, ItemToTake item);

        Task UpdateItem(Guid itemId, ItemToTake item);

        Task SetIsItemTaken(Guid itemId, bool isTaken);

        Task DeleteItem(Guid itemId);
    }
}
