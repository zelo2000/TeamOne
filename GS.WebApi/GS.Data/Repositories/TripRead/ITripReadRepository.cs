﻿using GS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Data.Repositories.TripRead
{
    public interface ITripReadRepository
    {
        Task<IEnumerable<Trip>> GetUserTrips(Guid userId);

        Task<IEnumerable<Trip>> GetTripById(Guid tripId);

        Task<IEnumerable<ToDoNode>> GetToDoNodes(Guid tripId);

        Task<IEnumerable<ItemToTake>> GetItemsToTake(Guid tripId);
    }
}
