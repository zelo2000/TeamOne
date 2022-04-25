using GS.Business.Command;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Business.Test.Command
{
    public class SetToDoNodeStatusCommandTest
    {
        private Mock<ITripWriteRepository> _tripWriteRepository;
        private SetToDoNodeStatusCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripWriteRepository = new Mock<ITripWriteRepository>();
            _handler = new SetToDoNodeStatusCommandHandler(_tripWriteRepository.Object);
        }

        [Test]
        public void AddItemCommand_ShouldAddItem()
        {
            var id = Guid.NewGuid();
            var status = NodeStatus.ToDo;

            _tripWriteRepository.Setup(x => x.SetToDoNodeStatus(It.IsAny<Guid>(), It.IsAny<NodeStatus>()))
                .Returns(Task.CompletedTask);

            var command = new SetToDoNodeStatusCommand(id, status);

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(command));
        }
    }
}
