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
    public class GetToDoNodesQueryTest
    {
        private Mock<ITripReadRepository> _tripReadRepository;
        private GetToDoNodesQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripReadRepository = new Mock<ITripReadRepository>();
            _handler = new GetToDoNodesQueryHandler(_tripReadRepository.Object);
        }

        [Test]
        public async Task GetToDoNodes_TripId_ListOfNodes()
        {
            var id = Guid.NewGuid();

            var items = new List<ToDoNode>()
            {
                new ToDoNode
                {
                    Id = Guid.NewGuid()
                },
                new ToDoNode
                {
                    Id = Guid.NewGuid()
                }
            };

            _tripReadRepository.Setup(x => x.GetToDoNodes(It.IsAny<Guid>()))
                .ReturnsAsync(items);

            var query = new GetToDoNodesQuery(id);
            var result = await _handler.Handle(query);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}
