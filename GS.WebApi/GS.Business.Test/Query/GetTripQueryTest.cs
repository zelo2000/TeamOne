using FluentAssertions;
using GS.Business.Query;
using GS.Data.Entities;
using GS.Data.Repositories.TripRead;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Business.Test.Query
{
    public class GetTripQueryTest
    {
        private Mock<ITripReadRepository> _tripReadRepository;
        private GetTripQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripReadRepository = new Mock<ITripReadRepository>();
            _handler = new GetTripQueryHandler(_tripReadRepository.Object);
        }

        [Test]
        public async Task GetItemsToTake_TripId_ListOfItems()
        {
            var id = Guid.NewGuid();

            var trip = new Trip
            {
                Id = Guid.Empty
            };

            _tripReadRepository.Setup(x => x.GetTripById(It.IsAny<Guid>()))
                .ReturnsAsync(trip);

            var query = new GetTripQuery(id);
            var result = await _handler.Handle(query);

            result.Should().NotBeNull();
            result.Id.Should().Be(Guid.Empty);
        }
    }
}
