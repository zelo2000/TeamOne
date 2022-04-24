using GS.Business.Command;
using GS.Data.Entities;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Models.ItemToTake;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Business.Test.Command
{
    public class AddItemCommandTest
    {
        private Mock<ITripWriteRepository> _tripWriteRepository;
        private AddItemCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripWriteRepository = new Mock<ITripWriteRepository>();
            _handler = new AddItemCommandHandler(_tripWriteRepository.Object);
        }

        [Test]
        public void AddItemCommand_ShouldAddItem()
        {
            var id = Guid.NewGuid();
            var model = new ItemToTakeBaseModel();

            _tripWriteRepository.Setup(x => x.AddItem(It.IsAny<Guid>(), It.IsAny<ItemToTake>()))
                .Returns(Task.CompletedTask);

            var command = new AddItemCommand(id, model);

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(command));
        }
    }
}
