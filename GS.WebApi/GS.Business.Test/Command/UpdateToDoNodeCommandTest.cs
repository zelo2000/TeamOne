using GS.Business.Command;
using GS.Data.Entities;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Models.ToDoNode;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Business.Test.Command
{
    public class UpdateToDoNodeCommandTest
    {
        private Mock<ITripWriteRepository> _tripWriteRepository;
        private UpdateToDoNodeCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripWriteRepository = new Mock<ITripWriteRepository>();
            _handler = new UpdateToDoNodeCommandHandler(_tripWriteRepository.Object);
        }

        [Test]
        public void UpdateItemCommand_ShouldUpdateItem()
        {
            var id = Guid.NewGuid();
            var model = new ToDoNodeBaseModel();

            _tripWriteRepository.Setup(x => x.UpdateToDoNode(It.IsAny<Guid>(), It.IsAny<ToDoNode>()))
                .Returns(Task.CompletedTask);

            var command = new UpdateToDoNodeCommand(id, model);

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(command));
        }
    }
}
