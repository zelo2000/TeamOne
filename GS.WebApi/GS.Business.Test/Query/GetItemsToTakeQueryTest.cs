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
    public class GetItemsToTakeQueryTest
    {
        private Mock<ITripReadRepository> _tripReadRepository;
        private GetItemsToTakeQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripReadRepository = new Mock<ITripReadRepository>();
            _handler = new GetItemsToTakeQueryHandler(_tripReadRepository.Object);
        }

        [Test]
        public async Task GetItemsToTake_TripId_ListOfItems()
        {
            var id = Guid.NewGuid();

            var items = new List<ItemToTake>()
            {
                new ItemToTake
                {
                    Id = Guid.NewGuid()
                },
                new ItemToTake
                {
                    Id = Guid.NewGuid()
                }
            };

            _tripReadRepository.Setup(x => x.GetItemsToTake(It.IsAny<Guid>()))
                .ReturnsAsync(items);

            var query = new GetItemsToTakeQuery(id);
            var result = await _handler.Handle(query);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}
