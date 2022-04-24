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
    public class AddToDoNodeCommandTest
    {
        private Mock<ITripWriteRepository> _tripWriteRepository;
        private AddToDoNodeCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripWriteRepository = new Mock<ITripWriteRepository>();
            _handler = new AddToDoNodeCommandHandler(_tripWriteRepository.Object);
        }

        [Test]
        public void AddItemCommand_ShouldAddItem()
        {
            var id = Guid.NewGuid();
            var model = new ToDoNodeBaseModel();

            _tripWriteRepository.Setup(x => x.AddItem(It.IsAny<Guid>(), It.IsAny<ItemToTake>()))
                .Returns(Task.CompletedTask);

            var command = new AddToDoNodeCommand(id, model);

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(command));
        }
    }
}
