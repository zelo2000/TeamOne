using FluentAssertions;
using GS.Business.Query;
using GS.Data.Entities;
using GS.Data.Repositories.TripRead;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Business.Test.Query
{
    public class GetUserTripsQueryTest
    {
        private Mock<ITripReadRepository> _tripReadRepository;
        private GetTripsUserQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripReadRepository = new Mock<ITripReadRepository>();
            _handler = new GetTripsUserQueryHandler(_tripReadRepository.Object);
        }

        [Test]
        public async Task GetToDoNodes_TripId_ListOfNodes()
        {
            var id = Guid.NewGuid();

            var items = new List<Trip>()
            {
                new Trip
                {
                    Id = Guid.NewGuid()
                },
                new Trip
                {
                    Id = Guid.NewGuid()
                }
            };

            _tripReadRepository.Setup(x => x.GetUserTrips(It.IsAny<Guid>()))
                .ReturnsAsync(items);

            var query = new GetUserTripsQuery(id);
            var result = await _handler.Handle(query);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}
